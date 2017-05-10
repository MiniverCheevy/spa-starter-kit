using System;
using Voodoo.CodeGeneration.Helpers;
using Voodoo.CodeGeneration.Operations;
using Voodoo.Logging;

namespace Voodoo.CodeGeneration
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var request = new StartupRequest
            {
                Arguments = Environment.GetCommandLineArgs(),
                Path = StartupHelper.GetPath()
            };

            var response = new StartupFromConfig(request).Execute();
            Console.WriteLine(!response.IsOk ? response.Message : "Done");

            foreach (var log in Vs.Helper.Log)
            {
                if (log != null)
                    switch (log.Level)
                    {
                        case LogLevels.Error:
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            break;
                        case LogLevels.Info:
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                        case LogLevels.Trace:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            break;
                    }
                if (log.Message != null)
                    Console.WriteLine(log.Message);
            }
            ;
        }
    }
}