using Voodoo.CodeGeneration.Models;
using Voodoo.CodeGeneration.Models.Reflection;
using Voodoo.CodeGeneration.Models.Rest;
using Voodoo.CodeGeneration.Models.VisualStudio;

namespace Voodoo.CodeGeneration.Templates.PCL
{
    public partial class ServiceTemplate
    {
        public ServiceFile File { get; set; }
    }

    public class ServiceFile : TypedCodeFile
    {
        public Resource Resource { get; set; }

        public ServiceTemplate Template { get; set; }

        public override string FileName => $"{Name}.generated.cs";

        public ServiceFile(ProjectFacade project, TypeFacade type, Resource resource)
            : base(project, type)
        {
            Template = new ServiceTemplate {File = this};

            Name = $"{resource.Name}Service";
            Resource = resource;
            OverwriteExistingFile = true;
        }

        public override string GetFileContents()
        {
            return Template.TransformText();
        }

        public override string GetFolder()
        {
            return "Services.Generated";
        }
    }
}