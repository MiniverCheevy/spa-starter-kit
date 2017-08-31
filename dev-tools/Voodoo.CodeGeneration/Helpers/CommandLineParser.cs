using System;
using System.Collections.Generic;
using System.Linq;

namespace Voodoo.CodeGeneration.Helpers
{
    internal static class CommandLineParser
    {
        private static CommandHelper commands = new CommandHelper();

        internal static void ParseAndExecute(string[] args)
        {
            args = handleFlags(args);
            if (args.Length == 0)
            {
                showHelp();
                return;
            }
           
            if (!Vs.Helper.HasConfig)
                throw new Exception("Cannot find config file");

            var command = args[0].ToLower();
            string[] target = null;
            if (args.Length > 1)
                target = args.Except(new[] {command}).ToArray();

            commands.ExecuteCommand(command, target);
        }

        private static string[] handleFlags(string[] args)
        {
            var returnedArgs = new List<string>();
            foreach (var arg in args)
            {
                var test = arg.To<string>().ToLower().Trim();
                switch (test)
                {
                    case "-e":
                    case "-empty":
                        Vs.Helper.Flags.IsEmptyType = true;
                        break;

                    default:
                        returnedArgs.Add(arg);
                        break;
                }
            }
            return returnedArgs.ToArray();
        }

      

        private static void colorOff()
        {
            Console.ResetColor();
        }

        private static void colorOn()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
        }

        private static void showHelp()
        {
            Console.WriteLine(string.Empty);
            Console.WriteLine("spawn <command> target");
            Console.WriteLine(string.Empty);
            Console.WriteLine("Available commands are:");
            Console.WriteLine(string.Empty);
            colorOn();

            Console.WriteLine(string.Empty);
            colorOn();
            Console.WriteLine(
                "clean - deletes bin and obj folders of solution");
            colorOff();
            Console.WriteLine(string.Empty);
            commands.ShowHelp();
            Console.WriteLine(string.Empty);
            Console.WriteLine(
                "Enums and entities with Id and Name will be added to the list helper.  IsActive and SortOrder will be honored if available.");
            Console.WriteLine(string.Empty);
            Console.WriteLine(
                "<entity> refers to the type name of a class in the Data project");
            Console.WriteLine("[entity] is optional");
            Console.WriteLine(string.Empty);
            Console.WriteLine(
                "<message> refers to the type name of a class in the Logic project");
            Console.WriteLine("[message] is optional");
            Console.WriteLine(string.Empty);
            Console.WriteLine(
                "<class> refers to the type name of a class in any project");
            Console.WriteLine("[class] is optional");
            Console.WriteLine(
                "<operation> refers to a command or query");
            Console.WriteLine("[operation] is optional");
            Console.WriteLine(string.Empty);
            Console.WriteLine("fully qualified type name not needed if class name is unique");
            Console.WriteLine(string.Empty);
            Console.WriteLine("     example (command prompt): spawn logic MyProject.Models.Person");
            Console.WriteLine("                               spawn logic person");
            Console.WriteLine("                               ");
            Console.WriteLine("Use the Rest, MapsTo, Client and Secret attributes to control code generation");

            Console.WriteLine(string.Empty);
        }
    }
}