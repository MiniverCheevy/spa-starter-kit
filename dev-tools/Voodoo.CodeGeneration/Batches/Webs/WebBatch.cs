﻿using System.Linq;
using Voodoo.CodeGeneration.Helpers;
using Voodoo.CodeGeneration.Infrastructure;
using Voodoo.CodeGeneration.Templates.Web.ProjectLevel;
using Voodoo.CodeGeneration.Templates.Web.ProjectLevel.WebFrameworks;

namespace Voodoo.CodeGeneration.Batches.Webs
{
    [Generates(Command = "web", Format = "web [message]",
        Notes = "reads from Logic, writes to Web, uses typescript if set in spawn.json javascript otherwise")]
    public class WebBatch : Batch
    {
        public WebBatch(string[] targetTypes = null)
            : base(targetTypes)
        {
            ThrowIfNotFound(Token.Web, Token.Logic);
            GetTargetFrom(Token.Logic, false);
        }

        public override void Build()
        {
            var restBuilder = new RestBuilder(logic, web);

            foreach (var item in restBuilder.Resources)
                web.AddFile(new ApiControllerFile(web, type, restBuilder.Resources));

            var modelTypes = new ClientModelFactory(logic, models).GetTypes();
            var api = WebFramworkBatchFactory.GetTypeServiceFile(web,
                restBuilder.Resources.OrderBy(c => c.Name).ToArray(),
                Vs.Helper.Solution.JsAppPathFromWebProjectRoot);
            if (api != null)
                web.AddFile(api);
            var webModels = WebFrameworkFileFactory.GetTypeScriptModelsFile(web, modelTypes.ToArray(),
                Vs.Helper.Solution.JsAppPathFromWebProjectRoot);
            if (webModels != null)
                web.AddFile(webModels);

            if (Vs.Helper.Solution.ContextType != null)
                addNameValuePairs();
        }
    }
}