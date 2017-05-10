using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Voodoo.CodeGeneration.Helpers
{
    internal class Cleaner
    {
        private static List<string> foldersToDelete;
        private static List<string> foldersToClear;
        private static readonly string[] binAndObj = {"bin", "obj"};

        public static void Clean(string solutionFolder)
        {
            foldersToDelete = new List<string>();
            foldersToClear = new List<string>();
            clearFolders();
            cleanObjAndBin(solutionFolder);
        }

        private static void clearFolders()
        {
            buildFolderList();
            foreach (var dir in foldersToClear.Distinct().ToArray())
            {
                if (!Directory.Exists(dir))
                    continue;

                Console.WriteLine("Clearing " + dir);

                foreach (var file in Directory.GetFiles(dir))
                {
                    var f = file;
                    tryAndDontThrow(() => { IoNic.KillFile(f); });
                }
                foreach (var directory in Directory.GetDirectories(dir))
                {
                    var d = directory;
                    tryAndDontThrow(() => { IoNic.KillDir(d); });
                }
            }
        }

        private static void buildFolderList()
        {
            var userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            var winFolder = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
            var localSettings = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            var studios = new[] {"9.0", "10.0", "11.0", "12.0", "14.0", "15.0"};

            foreach (var studio in studios)
            {
                addFolderToClear(localSettings, @"\Microsoft\VisualStudio\", studio, "ProjectAssemblies");
                addFolderToClear(localSettings, @"\Microsoft\VisualStudio\", studio, "ComponentModelCache");
            }
            var dotNets = new[] {"v1.1.4322", "", "v2.0.50727", "v4.0.30319"};
            foreach (var dotNet in dotNets)
            {
                addFolderToClear(winFolder,
                    @"\Microsoft.NET\Framework64\", dotNet, "Temporary ASP.NET Files");
                addFolderToClear(winFolder,
                    @"\Microsoft.NET\Framework\", dotNet, "Temporary ASP.NET Files");
            }

            addFolderToClear(Environment.GetFolderPath(Environment.SpecialFolder.InternetCache));
            addFolderToClear(localSettings, "Temp");
            addFolderToClear(winFolder, "Temp");
            addFolderToClear(Environment.GetEnvironmentVariable("TEMP"));
            addFolderToClear(Environment.GetEnvironmentVariable("TMP"));
            addFolderToClear(@"C:\TEMP");
            addFolderToClear(@"C:\FuLog");
            addFolderToClear(@"C:\Logs");
            addFolderToClear(winFolder, @"Logs");
            addFolderToClear(userFolder, @"Documents\IISExpress\TraceLogFiles");
            addFolderToClear(userFolder, @"Documents\IISExpress\Logs\");
            addFolderToClear(userFolder, @"AppData\Local\NCrunch");
            addFolderToClear(userFolder, @"AppData\Local\Microsoft\WebsiteCache");
        }

        private static void tryAndDontThrow(Action action)
        {
            try
            {
                action();
            }
            catch
            {
                // ignored
            }
        }

        private static void addFolderToClear(params string[] pathParts)
        {
            tryAndDontThrow(() =>
            {
                var slash = @"\";
                var output = new StringBuilder();
                string last = null;
                foreach (var item in pathParts)
                {
                    if (last != null && !last.EndsWith(slash) && !item.StartsWith(slash))
                        output.Append(slash);
                    last = item;
                    output.Append(item);
                }
                var path = output.ToString();
                foldersToClear.Add(path);
            });
        }

        private static void cleanObjAndBin(string solutionFolder)
        {
            if (solutionFolder.ToLower().Contains(@"c:\users"))
                return;
            findBinAndObjFolders(solutionFolder);
            foreach (var item in foldersToDelete)
                try
                {
                    IoNic.KillDir(item);
                    Console.WriteLine("Deleting " + item);
                }
                catch
                {
                    Console.WriteLine("Failed to Delete " + item);
                }
        }

        private static void findBinAndObjFolders(string directory)
        {
            foreach (var dir in Directory.GetDirectories(directory))
                tryAndDontThrow(() => { findBinAndObjFolders(dir); });
            var info = new DirectoryInfo(directory);
            if (binAndObj.Contains(info.Name.ToLower()))
                foldersToDelete.Add(directory);
        }
    }
}