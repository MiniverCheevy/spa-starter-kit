using System;
using System.IO;

namespace Voodoo.CodeGeneration.Helpers
{
    public static class StartupHelper
    {
        public static string GetPath()
        {
            var path = IoNic.ResolveRelativePath(Environment.CurrentDirectory);
            Console.WriteLine(path);

            if (path != null && Directory.Exists(path))
                if (File.Exists(IoNic.PathCombineLocal(IoNic.ResolveRelativePath(path), "spawn.dll")))
                    return path;

            path = IoNic.GetApplicationRootDirectory();
            if (path != null && Directory.Exists(path))
                if (File.Exists(IoNic.PathCombineLocal(IoNic.ResolveRelativePath(path), "spawn.dll")))
                    return path;

            path = Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);
            if (path != null && Directory.Exists(path))
                if (File.Exists(IoNic.PathCombineLocal(IoNic.ResolveRelativePath(path), "spawn.dll")))
                    return path;

            throw new Exception("Cannot determine working directory");
        }
    }
}