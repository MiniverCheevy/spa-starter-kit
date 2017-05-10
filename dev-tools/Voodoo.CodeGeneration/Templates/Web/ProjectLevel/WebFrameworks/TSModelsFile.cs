using System;
using Voodoo.CodeGeneration.Helpers.ModelBuilders;
using Voodoo.CodeGeneration.Models;
using Voodoo.CodeGeneration.Models.Rest;
using Voodoo.CodeGeneration.Models.VisualStudio;

namespace Voodoo.CodeGeneration.Templates.Web.ProjectLevel.WebFrameworks
{
    public abstract class TypeScriptServiceBatchFileBase : CodeFile
    {
        private string path;
        public Resource[] Resources { get; set; }

        public override string VisualStudioItemTypeNode => "TypeScriptCompile";
        public TypeScriptGraphBuilder Builder { get; set; } = new TypeScriptGraphBuilder();


        public override string FileName => "services.generated.ts";

        public string MessagesPath => WebFrameworkFileFactory.GetMessagesReferencePath();

        public string ModuleName => WebFrameworkFileFactory.GetModuleName();

        protected TypeScriptServiceBatchFileBase(ProjectFacade project, Resource[] resources, string path)
            : base(project)
        {
            OverwriteExistingFile = true;
            Resources = resources;
            this.path = path;
        }

        public override string GetFolder()
        {
            return $@"{path}";
        }
    }

    public abstract class TypeScriptModelsFileBase : CodeFile
    {
        private string path;

        public override string VisualStudioItemTypeNode => "TypeScriptCompile";
        public TypeScriptGraphBuilder Builder { get; set; } = new TypeScriptGraphBuilder();


        public override string FileName => "models.generated.ts";

        public string MessagesPath => WebFrameworkFileFactory.GetMessagesReferencePath();

        public string ModuleName => WebFrameworkFileFactory.GetModuleName();

        protected TypeScriptModelsFileBase(ProjectFacade project, Type[] types, string path) : base(project)
        {
            Builder.AddTypes(types);
            OverwriteExistingFile = true;
            this.path = path;
        }

        public override string GetFolder()
        {
            return $@"{path}\";
        }
    }
}