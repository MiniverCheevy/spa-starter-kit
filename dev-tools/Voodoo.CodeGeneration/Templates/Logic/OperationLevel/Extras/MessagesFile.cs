using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voodoo.CodeGeneration.Models;
using Voodoo.CodeGeneration.Models.VisualStudio;
using Voodoo;
using Voodoo.CodeGeneration.Models.Reflection;

namespace Voodoo.CodeGeneration.Templates.Logic.OperationLevel.Extras
{
    public partial class MessagesTemplate
    {
        public MessagesFile File { get; set; }
    }

    public class MessagesFile : TypedCodeFile
    {
        public MessagesFile(ProjectFacade project, TypeFacade type)
            : base(project, type)
        {
            FriendlyName = Name.ToFriendlyString();
            Template = new MessagesTemplate {File = this};
            Name = $"{Name}Messages";
            PageSpecificUsingStatements.Add(Type.SystemType.Namespace);
            Errors = type.Properties.SelectMany(c => c.ErrorMessages).Select(c => c.Text).ToArray();
            type.Properties.ForEach(c => PageSpecificUsingStatements.Add(c.PropertyType.Namespace));
        }

        public string FriendlyName { get; set; }
        public MessagesTemplate Template { get; set; }
        public string[] Errors { get; set; }

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