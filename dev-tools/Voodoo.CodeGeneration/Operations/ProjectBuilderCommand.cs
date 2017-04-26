//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Text.RegularExpressions;
//using Voodoo.CodeGeneration.Helpers;
//using Voodoo;
//using Voodoo.Infrastructure;
//using Voodoo.Operations;

//namespace Voodoo.CodeGeneration.Operations
//{
//	public class ProjectBuilderCommand : Command<BuilderRequest, BuilderResponse>
//	{
//		private readonly List<string> directories = new List<string>();
//		//private readonly string[] fileExtensions =
//		//{
//		//    ".cs", ".csproj", ".sln", ".bat", ".tt", ".aspx", ".ascx", ".config", ""
//		//};

//		private readonly string from = "Fernweh";
//		private List<string> files = new List<string>();
//		private string outPath = string.Empty;
//		private string path = string.Empty;
//		private string templateName = string.Empty;
//		private string to = string.Empty;

//		public ProjectBuilderCommand(BuilderRequest request) : base(request)
//		{
//		}

//		protected override void Validate()
//		{		
//			if (request.Args.Length < 2)
//				throw new LogicException("You must enter a template name and target name.");

//			if (string.IsNullOrWhiteSpace(request.Args[0]))
//				throw new LogicException("Template Name is required");

//			if (string.IsNullOrWhiteSpace(request.Args[1]))
//				throw new LogicException("Target Name is required");

//			base.Validate();
//		}

//		protected override BuilderResponse ProcessRequest()
//		{
//			to = request.Args[1];
//			templateName = request.Args[0];
//			PrepareTemplate();
//			DoCopy();
//			return response;
//		}

//		private void PrepareTemplate()
//		{
//			outPath = StartupHelper.GetPath();
//			path = IoNic.PathCombineLocal(Path.GetTempPath(), Guid.NewGuid().ToString().Replace("-", ""));
//			IoNic.MakeDir(path);
//			var zipName = string.Format("{0}.zip", templateName);
//			var zipPath = IoNic.PathCombineLocal(IoNic.GetApplicationRootDirectory(), "templates", zipName);
//			if (!File.Exists(zipPath))
//				throw new LogicException(string.Format("Could not find template {0}", templateName));

//			ZipHelper.ExtractZipFile(zipPath, path);
//			Console.WriteLine("Extracting to {0}", path);
//			path = IoNic.PathCombineLocal(path, templateName);
//		}


//		private void DoCopy()
//		{
//			CopyAll(new DirectoryInfo(path), new DirectoryInfo(outPath));
//			RenameAndRewriteFiles(outPath);
//			RenameDirectories(outPath);
//			foreach (var s in directories)
//			{
//				RenameAndRewriteFiles(s);
//			}
//		}

//		private void RenameAndRewriteFiles(string directory)
//		{
//			foreach (var s in Directory.GetFiles(directory))
//			{
//				var old = s;
//				var oldPath = old.Substring(0, old.LastIndexOf(@"\"));
//				var oldFile = old.Substring(old.LastIndexOf(@"\"));
//				var newFile = ReplaceEx(oldFile, from, to);
//				var newPath = oldPath + @"\" + newFile;
//				newPath = newPath.Replace(@"\\", @"\");
//				if (old != newPath)
//				{
//					if (File.Exists(newPath))
//					{
//						File.SetAttributes(newPath, FileAttributes.Normal);
//						File.Delete(newPath);
//					}
//					File.Move(old, newPath);
//				}
//				var extension = Path.GetExtension(newPath).ToLower();


//				if (extension == ".vspscc")
//				{
//					File.SetAttributes(newPath, FileAttributes.Normal);
//					File.Delete(newPath);
//				}
//				else
//				//if (fileExtensions.Contains(extension))
//				{
//					var contents = File.ReadAllText(newPath);
//					if (contents.ToLower().Contains(from.ToLower()))
//					{
//						contents = Regex.Replace(contents, from, to, RegexOptions.IgnoreCase);
//						File.SetAttributes(newPath, FileAttributes.Normal);
//						File.Delete(newPath);
//						using (var sw = File.CreateText(newPath))
//						{
//							sw.Write(contents);
//							sw.Flush();
//							sw.Close();
//						}
//					}
//				}
//			}
//		}

//		private void CopyAll(DirectoryInfo source, DirectoryInfo target)
//		{
//			if (Directory.Exists(target.FullName) == false)
//			{
//				Directory.CreateDirectory(target.FullName);
//			}

//			foreach (var file in source.GetFiles())
//			{
//				file.CopyTo(IoNic.PathCombineLocal(target.ToString(), file.Name), true);
//			}

//			foreach (var diSourceSubDir in source.GetDirectories())
//			{
//				var nextTargetSubDir =
//					target.CreateSubdirectory(diSourceSubDir.Name);
//				CopyAll(diSourceSubDir, nextTargetSubDir);
//			}
//		}

//		private void RenameDirectories(string dir)
//		{
//			foreach (var s in Directory.GetDirectories(dir))
//			{
//				var old = s;
//				var oldPath = old.Substring(0, old.LastIndexOf(@"\"));
//				var oldDir = old.Substring(old.LastIndexOf(@"\"));
//				var newDir = ReplaceEx(oldDir, from, to);
//				var newPath = oldPath + @"\" + newDir;
//				newPath = newPath.Replace(@"\\", @"\");
//				if (old != newPath)
//				{
//					Directory.Move(old, newPath);
//				}
//				if (!directories.Contains(newPath))
//				{
//					directories.Add(newPath);
//					RenameDirectories(newPath);
//				}
//			}
//		}

//		private string ReplaceEx(string original,
//			string pattern, string replacement)
//		{
//			int position0, position1;
//			var count = position0 = position1 = 0;
//			var upperString = original.ToUpper();
//			var upperPattern = pattern.ToUpper();
//			var inc = (original.Length/pattern.Length)*
//			          (replacement.Length - pattern.Length);
//			var chars = new char[original.Length + Math.Max(0, inc)];
//			while ((position1 = upperString.IndexOf(upperPattern,
//				position0)) != -1)
//			{
//				for (var i = position0; i < position1; ++i)
//					chars[count++] = original[i];
//				foreach (var t in replacement)
//					chars[count++] = t;
//				position0 = position1 + pattern.Length;
//			}
//			if (position0 == 0) return original;
//			for (var i = position0; i < original.Length; ++i)
//				chars[count++] = original[i];
//			return new string(chars, 0, count);
//		}
//	}
//}

