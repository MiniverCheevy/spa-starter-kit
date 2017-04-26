using System;
using System.Collections.Generic;
using System.Linq;
using Voodoo.CodeGeneration.Helpers;
using Voodoo.CodeGeneration.Models;
using Voodoo.CodeGeneration.Models.Reflection;
using Voodoo.CodeGeneration.Models.Rest;
using Voodoo.CodeGeneration.Models.VisualStudio;

namespace Voodoo.CodeGeneration.Templates.Web.ProjectLevel
{
    public partial class ApiControllerTemplate
    {
        public ApiControllerFile File { get; set; }
    }

    public class ApiControllerFile : TypedCodeFile
    {
        public ApiControllerFile(ProjectFacade project, TypeFacade type)
            : base(project, type)
        {
            Template = new ApiControllerTemplate {File = this};
            Name = $"{Name}Controller";
        }

        public ApiControllerFile(ProjectFacade project, TypeFacade type, IEnumerable<Resource> resources)
            : base(project, type)
        {
            this.Resources = resources;
            OverwriteExistingFile = true;
            Template = new ApiControllerTemplate {File = this};
            PageSpecificUsingStatements.Add("Voodoo.Messages");
            foreach (var resource in resources)
            {
                foreach (var verb in resource.Verbs)
                {
                    addNamespaces(verb.OperationType);
                    addNamespaces(verb.RequestType);
                    addNamespaces(verb.ResponseType);
                }
            }
        }

        public IEnumerable<Resource> Resources { get; set; }
        public ApiControllerTemplate Template { get; set; }

        public override string GetFileContents()
        {
            return Template.TransformText();
        }

        public override string FileName => "Api.generated.cs";

        public override string GetFolder()
        {
            return @"Controllers\Api\";
        }

        private void addNamespaces(Type type)
        {
            if (type == null)
                return;

            PageSpecificUsingStatements.Add(type.Namespace);
            if (type.GenericTypeArguments.Any())
            {
                foreach (var arg in type.GenericTypeArguments)
                {
                    addNamespaces(arg);
                }
            }
        }
    }
}