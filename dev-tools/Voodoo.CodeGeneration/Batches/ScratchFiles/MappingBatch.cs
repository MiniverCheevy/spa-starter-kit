using System;
using System.Linq;
using Voodoo.CodeGeneration.Helpers;
using Voodoo.CodeGeneration.Infrastructure;
using Voodoo.CodeGeneration.Models.Reflection;
using Voodoo.CodeGeneration.Templates.Scratch;

namespace Voodoo.CodeGeneration.Batches.ScratchFiles
{
    [Generates(Command = "mapping", Format = "mapping <class> <class>",
        Notes = "generates mapping between to classes, opens in updateor, not added to project")]
    public class MappingBatch : Batch
    {
        public MappingBatch(string[] targetTypes = null)
            : base(targetTypes)
        {
        }

        public override void Build()
        {
            if (allTargets.Count() < 2)
                throw new Exception("Mapping requires two types");

            var leftName = allTargets.First();
            var rightName = allTargets.Skip(1).Take(1).First();
            TypeFacade left = null;
            TypeFacade right = null;
            foreach (var project in Vs.Helper.Projects)
            {
                var type = project.FindType(leftName);
                if (type != null)
                    left = new TypeFacade(type);

                type = project.FindType(rightName);
                if (type != null)
                    right = new TypeFacade(type);

                if (left != null && right != null)
                    break;
            }

            if (left == null)
                throw new Exception(string.Format("Cannot find {0}", leftName));
            if (right == null)
                throw new Exception(string.Format("Cannot find {0}", rightName));

            Vs.Helper.ScratchFiles.Add(new MappingFile(left, right));
        }
    }
}