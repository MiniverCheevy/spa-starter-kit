﻿using Voodoo.CodeGeneration.Models;
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
        public bool HasDetail { get; set; }
        public MessageTemplate Template { get; set; }

        public MessageFile(ProjectFacade project, TypeFacade type)
            : base(project, type)
        {
            Template = new MessageTemplate {File = this};
            PageSpecificUsingStatements.Add("Voodoo.Validation");
            Name = $"{Name}Message";
            HasDetail = type.HasDetailFlag;
            PageSpecificUsingStatements.Add(type.Namespace);


            PageSpecificUsingStatements.Add("System");
            PageSpecificUsingStatements.Add("System.Collections.Generic");
            PageSpecificUsingStatements.Add("System.ComponentModel.DataAnnotations");
            PageSpecificUsingStatements.Add("System.Linq");
            PageSpecificUsingStatements.Add("Voodoo.Messages");
            PageSpecificUsingStatements.Add("Voodoo.Infrastructure.Notations");

            type.Properties.ForEach(c => PageSpecificUsingStatements.Add(c.PropertyType.Namespace));
        }

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