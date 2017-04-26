using Voodoo.CodeGeneration.Helpers;
using Voodoo.CodeGeneration.Models;
using Voodoo.CodeGeneration.Models.Reflection;
using Voodoo.CodeGeneration.Models.Rest;
using Voodoo.CodeGeneration.Models.VisualStudio;

namespace Voodoo.CodeGeneration.Templates.Web.ProjectLevel.WebFrameworks.Angular
{
    public partial class JsServiceTemplate
    {
        public JsServiceFile File { get; set; }
    }

    public class JsServiceFile : TypedCodeFile
    {
        private string path;

        public JsServiceFile(ProjectFacade project, TypeFacade type, Resource resource, string path)
            : base(project, type)
        {
            Name = resource.Name;
            Template = new JsServiceTemplate {File = this};
            JsName = firstLetterLower(Name);
            Name = string.Format("{0}Factory", Name);
            Resource = resource;
            OverwriteExistingFile = true;
            this.path = path;
            Template = new JsServiceTemplate {File = this};
        }

        public string[] BodyVerbs
        {
            get { return new[] {"post", "put"}; }
        }

        public override string VisualStudioItemTypeNode
        {
            get { return VisualStudioItemType.Content.ToString(); }
        }

        public override string FileName
        {
            get { return string.Format("{0}.generated.js", JsName); }
        }

        public JsServiceTemplate Template { get; set; }
        public Resource Resource { get; set; }
        public string JsName { get; set; }

        public override string GetFileContents()
        {
            return Template.TransformText();
        }

        public override string GetFolder()
        {
            return $@"{path}\services.generated\";
        }

        private string firstLetterLower(string value)
        {
            return char.ToLower(value[0]) + value.Substring(1);
        }
    }
}