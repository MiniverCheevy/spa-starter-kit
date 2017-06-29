using System;
using System.Text;
using Voodoo.CodeGeneration.Helpers;
using Voodoo.CodeGeneration.Models.VisualStudio;

namespace Voodoo.CodeGeneration.Templates.Web.ProjectLevel.WebFrameworks.Aurelia
{

    public class TypeScriptModelsFile : TypeScriptModelsFileBase
    {

        public TypeScriptModelsFile(ProjectFacade project, Type[] types, string path) : base(project, types, path)
        {
            Builder.AddTypes(types);
            OverwriteExistingFile = true;
        }

        public override string GetFileContents()
        {
            var output = new StringBuilder();

            output.AppendLine(TextBlocks.HeaderComment);
            output.AppendLine();
            output.AppendLine(Builder.GetOutput());


            return CodeFormatter.Format(output.ToString());
        }
    }
}