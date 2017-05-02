using Voodoo.CodeGeneration.Models;
using Voodoo.CodeGeneration.Models.Reflection;
using Voodoo.CodeGeneration.Models.VisualStudio;

namespace Voodoo.CodeGeneration.Templates.Logic.OperationLevel.Extras
{
    public partial class MessageTemplate
    {
        public MessageFile File { get; set; }
    }

    public class MessageFile : TypedCodeFile
    {
        public MessageFile(ProjectFacade project, TypeFacade type)
            : base(project, type)
        {
            Template = new MessageTemplate {File = this};
            PageSpecificUsingStatements.Add("Voodoo.Validation");
            Name = $"{Name}Message";
            HasDetail = type.HasDetailFlag;
            PageSpecificUsingStatements.Add(type.Namespace);
            type.Properties.ForEach(c => PageSpecificUsingStatements.Add(c.PropertyType.Namespace));
        }

        public bool HasDetail { get; set; }
        public MessageTemplate Template { get; set; }

        public override string GetFileContents()
        {
            return Template.TransformText();
        }

        public override string GetFolder()
        {
            return $@"Operations\{ExtrasFolder}";
        }
    }
}