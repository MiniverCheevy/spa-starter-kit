using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voodoo.CodeGeneration.Models;
using Voodoo.CodeGeneration.Models.VisualStudio;


namespace Voodoo.CodeGeneration.Templates.Web.ProjectLevel
{
    public partial class CustomAuthorizeAttributeTemplate
    {
        public CustomAuthorizeAttributeFile File { get; set; }
    }

    public class CustomAuthorizeAttributeFile : CodeFile
    {
        public CustomAuthorizeAttributeFile(ProjectFacade project)
            : base(project)
        {
            Template = new CustomAuthorizeAttributeTemplate {File = this};
            Name = "CustomAuthorizeAttribute";
            OverwriteExistingFile = false;
        }

        public CustomAuthorizeAttributeTemplate Template { get; set; }

        public override string GetFileContents()
        {
            return Template.TransformText();
        }

        public override string GetFolder()
        {
            return @"Controllers\Api\";
        }
    }
}