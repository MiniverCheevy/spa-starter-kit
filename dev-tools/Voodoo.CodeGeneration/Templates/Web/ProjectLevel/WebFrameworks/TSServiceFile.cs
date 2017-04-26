﻿using Voodoo.CodeGeneration.Helpers;
using Voodoo.CodeGeneration.Helpers.ModelBuilders;
using Voodoo.CodeGeneration.Models;
using Voodoo.CodeGeneration.Models.Rest;
using Voodoo.CodeGeneration.Models.VisualStudio;
using Voodoo.CodeGeneration.Templates.Web.ProjectLevel.WebFrameworks.Angular;

namespace Voodoo.CodeGeneration.Templates.Web.ProjectLevel.WebFrameworks
{
    public abstract class TsServiceFileBase : CodeFile
    {
        private string path;

        protected TsServiceFileBase(ProjectFacade project, Resource resource, string path)
            : base(project)
        {
            Name = resource.Name;
            TsName = Name;
            Name = $"{Name}Factory";
            Resource = resource;
            OverwriteExistingFile = true;
            this.path = path;
            Builder = new TypeScriptGraphBuilder();
        }

        public TypeScriptGraphBuilder Builder { get; set; }
        public string[] BodyVerbs => new[] {"post", "put"};
        public override string VisualStudioItemTypeNode => "TypeScriptCompile";
        public override string FileName => $"{TsName}.generated.ts";

        public Resource Resource { get; set; }
        public string TsName { get; set; }

        public override string GetFolder()
        {
            return $@"{path}\api.generated\";
        }

        private string firstLetterLower(string value)
        {
            return char.ToLower(value[0]) + value.Substring(1);
        }
    }
}