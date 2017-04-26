//using System;
//using System.Linq;
//using Voodoo.CodeGeneration.Helpers;
//using Microsoft.TeamFoundation.Client;
//using Microsoft.TeamFoundation.Server;
//using Microsoft.TeamFoundation.VersionControl.Client;

//namespace Voodoo.CodeGeneration.Models.SourceControl
//{
//	//http://singhtechies.com/programatically-checkin-file-to-tfs-using-c/
//	public class TfsSourceControlProvider : ISourceControlProvider
//	{
//		private readonly Workspace workspace;

//		public TfsSourceControlProvider()
//		{
//			var info = Workstation.Current.GetLocalWorkspaceInfo(Vs.Helper.SolutionFolder);
//			if (info == null)
//			{
//				Console.WriteLine("Cannot Find Tfs Workspace at " + Vs.Helper.SolutionFolder);
//				return;
//			}
//			var uri = info.ServerUri;

//			var projectCollection = TfsTeamProjectCollectionFactory.GetTeamProjectCollection(uri);
//			var cssService = projectCollection.GetService<ICommonStructureService4>();
//			workspace = info.GetWorkspace(projectCollection);
//			IsActive = true;
//		}

//		public bool IsActive { get; set; }

//		public void CheckOutFiles(params string[] paths)
//		{
//			if (workspace == null || !paths.Any())
//				return;
//			workspace.PendEdit(paths);
//		}

//		public void AddFiles(params string[] paths)
//		{
//			if (workspace == null || !paths.Any())
//				return;
//			var count = workspace.PendAdd(paths);
//		}
//	}
//}

