using System;
using System.Linq;
using Voodoo.CodeGeneration.Helpers;
using Voodoo.CodeGeneration.Models.Reflection;
using Voodoo.CodeGeneration.Models.VisualStudio;
using Voodoo.CodeGeneration.Templates.Logic.ProjectLevel.NameValuePairs;

namespace Voodoo.CodeGeneration.Batches
{
    public abstract class Batch
    {
        protected string[] allTargets;
        protected Type contextType;
        protected string js;

        protected string targetTypeName;
        protected TypeFacade type;
        protected ProjectFacade data => Vs.Helper.Solution.DataProject;
        protected ProjectFacade logic => Vs.Helper.Solution.LogicProject;
        protected ProjectFacade tests => Vs.Helper.Solution.TestProject;
        protected ProjectFacade web => Vs.Helper.Solution.WebProject;
        protected ProjectFacade pcl => Vs.Helper.Solution.PCLProject;
        protected ProjectFacade ionic => Vs.Helper.Solution.IonicProject;

        protected Batch(string[] targetTypes = null)
        {
            initialize();
            if (targetTypes != null)
            {
                targetTypeName = targetTypes.FirstOrDefault();
                allTargets = targetTypes;
            }
        }

        public abstract void Build();

        protected void GetTargetFrom(Token token, bool throwIfNotFound = true)
        {
            Type type = null;
            var project = GetProjectFrom(token);
            if (targetTypeName != null)
                type = project.FindType(targetTypeName);
            if (type != null)
                this.type = new TypeFacade(type);
            if (type == null && throwIfNotFound && Vs.Helper.Flags.IsEmptyType)
            {
                this.type = TypeFacade.CreateEmptyType(targetTypeName);
                return;
            }
            if (type == null && throwIfNotFound)
                throw new Exception(
                    $"Count not find {targetTypeName} in {token} project, make sure you've built recently in Debug Mode");
        }

        protected ProjectFacade GetProjectFrom(Token token)
        {
            switch (token)
            {
                case Token.Logic:
                    return logic;
                case Token.Tests:
                    return tests;
                case Token.Web:
                    return web;
                case Token.Data:
                    return data;
                case Token.Pcl:
                    return pcl;
                default:
                    throw new Exception($"Cannot load project {token}");
            }
        }

        private bool validate(Token token)
        {
            switch (token)
            {
                case Token.Pcl:
                    return pcl != null;
                case Token.Logic:
                    return logic != null;
                case Token.Tests:
                    return tests != null;
                case Token.Web:
                    return web != null;
                case Token.Data:
                    return data != null;
                case Token.Ionic:
                    return ionic != null;
                case Token.JsWebAppPath:
                    return !string.IsNullOrWhiteSpace(js);
                default:
                    throw new ArgumentOutOfRangeException(nameof(token));
            }
        }

        protected void ThrowIfNotFound(params Token[] tokens)
        {
            foreach (var token in tokens)
                if (!validate(token))
                    throw new Exception($"{token} is not setup");
        }

        protected bool AreAllSet(params Token[] tokens)
        {
            foreach (var token in tokens)
                if (!validate(token))
                    return false;
            return true;
        }

        private void initialize()
        {
        }

        protected void addNameValuePairs()
        {
            var builder = new NameValuePairBuilder(contextType);
            var listTypes = builder.Build();
            logic.AddFile(new ListsResponseFile(logic, listTypes));
            logic.AddFile(new ListsEnumFile(logic, listTypes));
            logic.AddFile(new ListsRequestFile(logic));
            logic.AddFile(new ListsHelperFile(logic, listTypes));
        }
    }
}