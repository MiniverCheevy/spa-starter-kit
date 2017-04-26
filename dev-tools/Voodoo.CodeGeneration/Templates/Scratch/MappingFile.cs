using Voodoo.CodeGeneration.Helpers;
using Voodoo.CodeGeneration.Models;
using Voodoo.CodeGeneration.Models.Reflection;

namespace Voodoo.CodeGeneration.Templates.Scratch
{
    public partial class MappingTemplate
    {
        public MappingFile File { get; set; }
    }

    public class MappingFile : ScratchFile
    {
        public MappingFile(TypeFacade left, TypeFacade right)
        {
            Template = new MappingTemplate {File = this};
            Types = new TypeComparer(left, right);
        }

        public TypeComparer Types { get; set; }
        public MappingTemplate Template { get; set; }

        public override string GetFileContents()
        {
            return Template.TransformText();
        }
    }
}