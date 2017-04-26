using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voodoo;

namespace watch
{
	class Program
	{
		private static FileSystemWatcher watcher;

		static void Main(string[] args)
		{

			var directory = IoNic.GetApplicationRootDirectory();
			//directory = @"Z:\Dev\LGM\V1";
			watcher = new FileSystemWatcher(directory, "*.js");
			watcher.IncludeSubdirectories = true;
			watcher.Changed += Watcher_Changed;
			watcher.Created += Watcher_Changed;
			watcher.Deleted += Watcher_Changed;
			watcher.Renamed += Watcher_Changed;
			watcher.EnableRaisingEvents = true;
			Console.WriteLine("Press any key to exit");
			Console.ReadKey();
		}

		private static void Watcher_Changed(object sender, FileSystemEventArgs e)
		{
			watcher.EnableRaisingEvents = false;
			var startInfo = new ProcessStartInfo()
			{
				Arguments = "web",
				CreateNoWindow = true,
				FileName = "spawn",
				RedirectStandardOutput = false,
				Verb = "runas",
				UseShellExecute = true,
				WindowStyle = ProcessWindowStyle.Hidden
			};

			var process = new Process() { StartInfo = startInfo };
			process.Start();
			process.WaitForExit();
			Console.Write(".");
			watcher.EnableRaisingEvents = true;

		}
	}
}
