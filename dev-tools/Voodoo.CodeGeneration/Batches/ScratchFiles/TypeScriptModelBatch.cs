using System;
using System.Linq;
using Voodoo.CodeGeneration.Helpers;
using Voodoo.CodeGeneration.Models.Reflection;
using Voodoo.CodeGeneration.Templates.Scratch;

namespace Voodoo.CodeGeneration.Batches.ScratchFiles
{
    //[Generates(Command = "tsmodel", Format = "tsmodel <class>",
    //	Notes = "generates type script model for message, opens in editor, not added to project")]
    public class TypeScriptModelBatch : Batch
    {
        public TypeScriptModelBatch(string[] targetTypes = null)
            : base(targetTypes)
        {
        }

        public override void Build()
        {
            if (!allTargets.Any())
                throw new Exception("Mapping requires a types");

            var targetName = allTargets.First();
            TypeFacade target = null;

            foreach (var project in Vs.Helper.Projects)
            {
                var findType = project.FindType(targetName);
                if (findType != null)
                    target = new TypeFacade(findType);

                if (target != null)
                    break;
            }

            if (target == null)
                throw new Exception(string.Format("Cannot find {0}", targetName));

            Vs.Helper.ScratchFiles.Add(new TypeScriptModel(target));
        }
    }
}