using System;
using System.IO;
using Voodoo.CodeGeneration.Helpers;
using Voodoo.CodeGeneration.Models.VisualStudio;
using Voodoo.Messages;
using Voodoo.Operations;

namespace Voodoo.CodeGeneration.Operations
{
    public class Startup : Executor<SolutionFacade, Response>
    {
        public Startup(SolutionFacade request)
            : base(request)
        {
        }

        protected override Response ProcessRequest()
        {
            Console.WriteLine("Generator starting");
            Console.Write("Called from ");
            Console.WriteLine(getPath());

            Vs.Helper.SpawnPath = getPath();
            CommandLineParser.ParseAndExecute(request.CommandLineArguments);
            Cleanup();
            return response;
        }

        private string getPath()
        {
            var path = Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);
            if (path != null && Directory.Exists(path))
                return IoNic.ResolveRelativePath(path);

            path = IoNic.ResolveRelativePath(Environment.CurrentDirectory);

            return path;
        }

        private void Cleanup()
        {
        }
    }
}