using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Voodoo.CodeGeneration.Infrastructure;
using Voodoo.CodeGeneration.Models;
using Voodoo;
using Voodoo.CodeGeneration.Batches;

namespace Voodoo.CodeGeneration.Helpers
{
    public class CommandHelper
    {
        public CommandHelper()
        {
            Commands = getCommands(GetType().Assembly);
        }

        public List<GeneratorCommand> Commands { get; set; }

        private List<GeneratorCommand> getCommands(Assembly assembly)
        {
            var response = assembly.GetTypesSafetly()
                .Where(c => c.GetCustomAttribute(typeof(GeneratesAttribute)) != null)
                .Select(c => new GeneratorCommand(c))
                .ToArray()
                .Where(c => !string.IsNullOrWhiteSpace(c.Name))
                .ToList();
            return response;
        }

        public void ExecuteCommand(string commandName, string[] targetTypes)
        {
            commandName = commandName.ToLower().Trim();
            var command = Commands.FirstOrDefault(c => c.Name == commandName);
            if (command == null)
                throw new Exception($"Could not find command {commandName}");
            var batch = Activator.CreateInstance(command.BatchCommandType, new object[] {targetTypes}).To<Batch>();
            batch.Build();
            Console.WriteLine("Writing Files");
            foreach (var project in Vs.Helper.Projects)
            {
                if (project.Files.Any())
                {
                    project.WriteFiles();
                }
            }

            Vs.Helper.WriteScratchFiles();

            Vs.Helper.UnloadAllProjects();
        }

        private void colorOff()
        {
            Console.ResetColor();
        }

        private void colorOn()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
        }

        public void ShowHelp()
        {
            foreach (var command in Commands.OrderBy(c => c.Name))
            {
                Console.WriteLine(string.Empty);
                colorOn();
                Console.Write(command.Attribute.Format);
                colorOff();
                Console.WriteLine(string.Empty);
                writeLineIfNotNullOrWhiteSpace(command.Attribute.Notes, true);
            }
        }

        private void writeLineIfNotNullOrWhiteSpace(string message, bool indent = false, string prefix = null)
        {
            if (string.IsNullOrWhiteSpace(message))
                return;
            if (indent)
                Console.Write("     ");
            if (!string.IsNullOrWhiteSpace(prefix))
                Console.Write(prefix);
            Console.WriteLine(message);
        }
    }
}