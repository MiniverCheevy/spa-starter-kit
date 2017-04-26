using Voodoo.CodeGeneration.Models;
using Voodoo.CodeGeneration.Models.Reflection;
using Voodoo.CodeGeneration.Models.VisualStudio;

namespace Voodoo.CodeGeneration.Templates.Logic.ProjectLevel.NameValuePairs
{
	public partial class ListsQueryTemplate
	{
		public ListsQueryFile File { get; set; }
	}

	public class ListsQueryFile : TypedCodeFile
	{
		public ListsQueryFile(ProjectFacade project, TypeFacade type)
			: base(project, type)
		{
			Template = new ListsQueryTemplate {File = this};
			Name = string.Format("{0}ListsQuery", Name);
		}

		public ListsQueryTemplate Template { get; set; }

		public override string GetFileContents()
		{
			return Template.TransformText();
		}

		public override string GetFolder()
		{
			return @"Operations\Lists";
		}
	}
}