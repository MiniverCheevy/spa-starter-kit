namespace Voodoo.CodeGeneration.Models.Reflection
{
    public class GeneratedProperty
    {
        public TypeFacade ParentType { get; set; }
        public PropertyFacade Property { get; set; }
        public string PathToObject { get; set; }
        public string PropertyName { get; set; }
        public string StringifiedTypeName { get; set; }

        public override string ToString()
        {
            return $"{ParentType}.{Property}";
        }
    }
}