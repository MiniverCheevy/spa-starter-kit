using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Voodoo.CodeGeneration.Helpers;
using System.Threading.Tasks;

namespace Voodoo.CodeGeneration.Models.SourceControl
{
    public class TfExeSourceControlProvider : ISourceControlProvider
    {
        public bool IsActive { get; set; }

        public TfExeSourceControlProvider()
        {
            if (File.Exists(Vs.Helper.Solution.PathToTfDotExe))
            {
                IsActive = true;
            }
            else
            {
                Vs.Helper.Log.Add(new LogEntry { Message = $"Could not find tf.exe at {Vs.Helper.Solution.PathToTfDotExe}", Level = Logging.LogLevels.Error });
                if (findTfExe())
                    IsActive = true;

                return;
            }
                
        }

        private bool findTfExe()
        {
            var versions = Vs.Helper.VisualStudioVersions;
            foreach (var version in versions)
            {
                var path = $"C:\\Program Files (x86)\\Microsoft Visual Studio {version}\\Common7\\IDE\\TF.exe";
                if (File.Exists(path))
                {
                    Vs.Helper.Log.Add(new LogEntry { Message = $"Using tf.exe at {path}", Level = Logging.LogLevels.Info });
                    Vs.Helper.Solution.PathToTfDotExe = path;
                    return true;
                }
            }

            return false;
        }

        public Process GetProcess()
        {
            return new Process
            {
                StartInfo =
                    new ProcessStartInfo
                    {
                        FileName = Vs.Helper.Solution.PathToTfDotExe,
                        UseShellExecute = false,
                        RedirectStandardOutput = false,
                        CreateNoWindow = true,
                        WorkingDirectory = Vs.Helper.SolutionFolder,
                        WindowStyle = ProcessWindowStyle.Hidden
                    }
            };
        }

        public void AddFiles(params string[] files)
        {
            shell("add -I {0} ", files);
        }

        public void CheckOutFiles(params string[] files)
        {
            shell(" checkout {0} ", files);
        }

        public void shell(string arguments, string[] paths)
        {
            Parallel.ForEach(paths, (c) => shellone(arguments, c));
        }

        public void shellone(string arguments, string path)
        {
            try
            {
                var tfProcess = GetProcess();
                tfProcess.StartInfo.Arguments = string.Format(arguments, path);
                tfProcess.Start();
                tfProcess.WaitForExit();
                var exitCode = tfProcess.ExitCode;
                if (exitCode != 0 && exitCode != 100)
                    Vs.Helper.Log.Add(LogEntry.Error("ERROR {0} ,failed code: {1}", tfProcess.StartInfo.Arguments,
                        exitCode.ToString()));
            }
            catch (Exception ex)
            {
                File.SetAttributes(path, FileAttributes.Archive);
                Vs.Helper.Log.Add(LogEntry.Error("ERROR failed to add {0} error {1}", path, ex.Message));
            }
        }
    }
}