using Voodoo.CodeGeneration.Helpers.ModelBuilders;
using Voodoo.CodeGeneration.Models;
using Voodoo.CodeGeneration.Models.Reflection;

namespace Voodoo.CodeGeneration.Templates.Scratch
{
    public class TypeScriptModel : ScratchFile
    {
        public TypeFacade targetType { get; set; }

        public TypeScriptModel(TypeFacade target)
        {
            targetType = target;
        }

        public override string GetFileContents()
        {
            return new TypeScriptModelBuilder().GenerateDeclaration(targetType.SystemType);
        }
    }
}