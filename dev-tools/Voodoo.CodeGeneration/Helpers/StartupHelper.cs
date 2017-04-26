using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voodoo;

namespace Voodoo.CodeGeneration.Helpers
{
    public static class StartupHelper
    {
        public static string GetPath()
        {
            var path = IoNic.ResolveRelativePath(Environment.CurrentDirectory);

            if (path != null && Directory.Exists(path))
            {
                if (File.Exists(IoNic.PathCombineLocal(IoNic.ResolveRelativePath(path), "spawn.exe")))
                {
                    return path;
                }
            }

            path = IoNic.GetApplicationRootDirectory();
            if (path != null && Directory.Exists(path))
            {
                if (File.Exists(IoNic.PathCombineLocal(IoNic.ResolveRelativePath(path), "spawn.exe")))
                {
                    return path;
                }
            }

            path = Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);
            if (path != null && Directory.Exists(path))
            {
                if (File.Exists(IoNic.PathCombineLocal(IoNic.ResolveRelativePath(path), "spawn.exe")))
                {
                    return path;
                }
            }

            throw new Exception("Cannot determine working directory");
        }
    }
}