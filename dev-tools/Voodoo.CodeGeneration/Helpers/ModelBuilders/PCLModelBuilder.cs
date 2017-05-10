using System;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Voodoo.CodeGeneration.Helpers.ModelBuilders
{
    public class PclModelBuilder : ModelBuilder
    {
        public override string RewriteTypeName(Type type)
        {
            return type.FixUpTypeName();
        }

        public override string GenerateDeclaration(Type modelType, params string[] exclusions)
        {
            exclusions = exclusions ?? new string[] { };

            var result = new StringBuilder();

            result.AppendFormat("public {0} ", modelType.Name);

            if (modelType.BaseType != null
                && modelType.BaseType != typeof(object))
                result.AppendFormat(": {0} ", modelType.BaseType.Name);

            result.Append("{");
            result.AppendLine();

            // Only get properties that are not derived
            var declaredProperties = modelType.GetProperties(
                BindingFlags.Public |
                BindingFlags.Instance |
                BindingFlags.DeclaredOnly
            );

            foreach (var property in declaredProperties)
            {
                if (exclusions.Contains(property.Name))
                    continue;

                var response = GetPropertyDeclaration(property);
                result.Append(response);
                result.AppendLine();
            }

            result.Append("}");

            return result.ToString();
        }

        public override string GetPropertyDeclaration(PropertyInfo property)
        {
            return $"public {property.PropertyType.FixUpTypeName()} {property.Name} {{get;set;}}";
        }
    }
}