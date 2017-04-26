using Voodoo.CodeGeneration.Helpers;
using Voodoo.CodeGeneration.Infrastructure;
using Voodoo.CodeGeneration.Templates.PCL;
using Voodoo.CodeGeneration.Templates.Web.ProjectLevel;
using Voodoo.CodeGeneration.Templates.Web.ProjectLevel.WebFrameworks;

namespace Voodoo.CodeGeneration.Batches.PCL
{
    [Generates(Command = "pcl", Format = "pcl",
        Notes = "reads from Logic, writes to Pcl, creates flattened, attributeless classes")]
    public class PclBatch : Batch
    {
        public PclBatch(string[] targetTypes = null)
            : base(targetTypes)
        {
            ThrowIfNotFound(Token.Pcl, Token.Logic);
            GetTargetFrom(Token.Logic, false);
        }

        public override void Build()
        {
            var modelTypes = new ClientModelFactory(logic).GetTypes();
            var models = new ModelsFile(pcl, modelTypes.ToArray());
            pcl.AddFile(models);

            var restBuilder = new RestBuilder(logic, null);

            foreach (var item in restBuilder.Resources)
            {
                pcl.AddFile(new ServiceFile(pcl, type, item));
            }
        }
    }
}