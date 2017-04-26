using System.Linq;
using Voodoo.CodeGeneration.Helpers;
using Voodoo.CodeGeneration.Infrastructure;
using Voodoo.CodeGeneration.Models.Reflection;
using Voodoo.CodeGeneration.Templates.Web.ProjectLevel;
using Voodoo.CodeGeneration.Templates.Web.ProjectLevel.WebFrameworks;
using Voodoo.CodeGeneration.Templates.Web.ProjectLevel.WebFrameworks.Aurelia;

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
            {
                web.AddFile(new ApiControllerFile(web, type, restBuilder.Resources));
            }

            var modelTypes = new ClientModelFactory(logic).GetTypes();
            web.AddFile(new TsServiceBatchFile(web, restBuilder.Resources.OrderBy(c => c.Name).ToArray(),
                Vs.Helper.Solution.JsAppPathFromWebProjectRoot));
            var models = WebFrameworkFileFactory.GetTypeScriptModelsFile(web, modelTypes.ToArray(),
                Vs.Helper.Solution.JsAppPathFromWebProjectRoot);
            if (models != null)
                web.AddFile(models);
        }
    }
}