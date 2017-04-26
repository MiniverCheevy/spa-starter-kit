using System;
using System.Reflection;

namespace Voodoo.CodeGeneration.Models.Reflection
{
    public class NameValuePairTypeInformation
    {
        public PropertyInfo DbSet { get; set; }
        public Type EntityType { get; set; }
        public TypeFacade Facade { get; set; }
        public string Name { get; set; }
        public string PluralName { get; set; }
        public bool IsEnum { get; set; }
    }
}