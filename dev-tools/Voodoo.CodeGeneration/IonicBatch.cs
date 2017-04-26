using Voodoo.CodeGeneration.Helpers;
using Voodoo.CodeGeneration.Infrastructure;
using Voodoo.CodeGeneration.Templates.Web.ProjectLevel;
using Voodoo.CodeGeneration.Templates.Web.ProjectLevel.WebFrameworks;

namespace Voodoo.CodeGeneration.Batches.Web
{
    [Generates(Command = "ionic", Format = "ionic [message]", Notes = "reads from Logic, writes to Ionic, uses typescript if set in spawn.json javascript otherwise")]
    public class IonicBatch : Batch
    {
        public IonicBatch(string[] targetTypes = null)
            : base(targetTypes)
        {
            ThrowIfNotFound(Token.Ionic, Token.Logic);
            GetTargetFrom(Token.Logic, false);
        }

        public override void Build()
        {
            var restBuilder = new RestBuilder(logic);

            foreach (var item in restBuilder.Resources)
            {
                if (Vs.Helper.Solution.IonicUseTypeScript)
                {
                    var service = WebFrameworkFileFactory.GetTypeScriptFile(ionic, type, item, Vs.Helper.Solution.IonicAppPathFromWebProjectRoot);
                    if (service != null)
                        ionic.AddFile(service);
                }
                else
                {
                    var service = WebFrameworkFileFactory.GetJavaScriptFile(ionic, type, item, Vs.Helper.Solution.IonicAppPathFromWebProjectRoot);
                    if (service != null)
                        ionic.AddFile(service);
                }
            }
            var modelTypes = new ClientModelFactory(logic).GetTypes();

            if (Vs.Helper.Solution.IonicUseTypeScript)
            {
                var models = WebFrameworkFileFactory.GetTypeScriptModelsFile(ionic, modelTypes.ToArray(), Vs.Helper.Solution.IonicAppPathFromWebProjectRoot);
                if (models != null)
                    ionic.AddFile(models);
            }
            
        }
    }
}