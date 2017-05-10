using System.Diagnostics;
using System.Linq;
using Voodoo.CodeGeneration.Models;

namespace Voodoo.CodeGeneration.Helpers
{
    public static class VisualStudioAutomator
    {
        private static Process visualStudioProcess;

        public static void OpenFileInVisualStudio(CodeFile file)
        {
            var vsProcess = getVsProcess();
            if (vsProcess != null)
            {
                vsProcess.StartInfo.Arguments = string.Format("/Edit {0}", file.FullPath);
                vsProcess.Start();
            }
            else
            {
                IoNic.ShellExecute(file.FullPath);
            }
        }

        private static Process getVsProcess()
        {
            if (visualStudioProcess != null)
                return visualStudioProcess;

            visualStudioProcess = Process.GetProcessesByName("devenv").FirstOrDefault();
            if (visualStudioProcess != null)
            {
                var vsPath = visualStudioProcess.MainModule.FileName;

                var vsProcess = new Process
                {
                    StartInfo =
                        new ProcessStartInfo
                        {
                            FileName = vsPath
                        }
                };
                return vsProcess;
            }
            return null;
        }
    }
}