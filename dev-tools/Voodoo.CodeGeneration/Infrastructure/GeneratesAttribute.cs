using System;

namespace Voodoo.CodeGeneration.Infrastructure
{
    [AttributeUsage(AttributeTargets.Class)]
    public class GeneratesAttribute : Attribute
    {
        public string Command { get; set; }
        public string Format { get; set; }
        public string Notes { get; set; }
    }
}