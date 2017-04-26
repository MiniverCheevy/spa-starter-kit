using System;
using System.Reflection;
using Voodoo.CodeGeneration.Infrastructure;
using Voodoo;

namespace Voodoo.CodeGeneration.Models
{
    public class GeneratorCommand
    {
        public GeneratorCommand(Type type)
        {
            Attribute = type.GetCustomAttribute(typeof(GeneratesAttribute)).To<GeneratesAttribute>();
            Name = Attribute.Command;
            BatchCommandType = type;
        }

        public string Name { get; set; }
        public GeneratesAttribute Attribute { get; set; }
        public Type BatchCommandType { get; set; }
    }
}