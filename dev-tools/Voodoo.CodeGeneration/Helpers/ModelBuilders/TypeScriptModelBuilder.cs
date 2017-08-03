using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Voodoo.CodeGeneration.Helpers.ModelBuilders
{
    //based on
    //https://bitbucket.org/JamesDiacono/jdiacono/src/97b5ac3b21fe/JDiacono/TypeScript?at=default

    public class TypeScriptModelBuilder : ModelBuilder
    {
        public static Dictionary<string, string> Mappings => new Dictionary<string, string>
        {
            { "System.DateTimeOffset", "DateTimeOffset" },
            { "DateTimeOffset", "DateTimeOffset" },
            {"System.Int16", "number"},
            {"System.Int32", "number"},
            {"System.Int64", "number"},
            {"System.UInt16", "number"},
            {"System.UInt32", "number"},
            {"System.UInt64", "number"},
            {"System.Decimal", "number"},
            {"System.Single", "number"},
            {"System.Double", "number"},
            {"System.Char", "string"},
            {"System.String", "string"},
            {"System.Boolean", "boolean"},
            {"System.DateTime", "DateTimeOffset"},
            {"System.Guid", "any"},

        };

        public override string RewriteTypeName(Type type)
        {
            if (type == typeof(byte[]))
                return "any";
            type = Nullable.GetUnderlyingType(type) ?? type;
            var name = type.FixUpTypeName();
            //if (!type.IsInterface && !type.IsEnum && !type.IsScalar())
            //    name = $"I{name}";

            if (Mappings.ContainsKey(type.FullName))
            {
                name = Mappings[type.FullName];
                return name;
            }
            var primitiveArrayName = type.FullName.TrimEnd(']').TrimEnd('[');
            if (Mappings.ContainsKey(primitiveArrayName) && type.GetInterface("IEnumerable") != null)
            {
                name = Mappings[primitiveArrayName] + "[]";
                return name;
            }
            if (type.GetInterface("IEnumerable") == null)
                return name.Replace("<", "Of").Replace(">", "");
            name = string.Empty;
            var elementType = type.GetGenericArguments().FirstOrDefault();

            name = elementType == null
                ? RewriteTypeName(type).Replace("[]", "")
                : RewriteTypeName(elementType);
            if (!name.Contains("[]"))
                name = $"{name}[]";

            return name;
        }

        public override string GenerateDeclaration(Type modelType, params string[] exclusions)
        {
            exclusions = exclusions ?? new string[] { };

            var result = new StringBuilder();

            result.AppendFormat("class {0} ", modelType.Name);

            if (modelType.BaseType != null
                && modelType.BaseType != typeof(object))
                result.AppendFormat("extends {0} ", modelType.BaseType.Name);

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
            var name = property.Name;

            var type = ConvertTypeName(property.PropertyType);
            return $" {LowerCaseFirstLetter(name)}? : {type};";
        }
        public string GetConstPropertyDeclaration(PropertyInfo property, bool isLast)
        {
            var name = property.Name;

            var lastChar = isLast ? string.Empty : ",";
            var type = ConvertTypeName(property.PropertyType);
            return $" {LowerCaseFirstLetter(name)}:null{lastChar}";
        }

        public string ConvertTypeName(Type type)
        {
            type = Nullable.GetUnderlyingType(type) ?? type;
            var name = RewriteTypeName(type);
            var family = GetTypeFamily(type);
            switch (family)
            {
                case TypeFamily.Enum:
                    return name;
                case TypeFamily.System:
                    if (Mappings.ContainsKey(type.FullName))
                        return name;

                    return "any";
                case TypeFamily.Collection:
                    name = string.Empty;
                    var elementType = type.GetGenericArguments().FirstOrDefault();

                    name = elementType == null
                        ? RewriteTypeName(type).Replace("[]", "")
                        : RewriteTypeName(elementType);
                    if (!name.Contains("[]"))
                        name = $"{name}[]";

                    return name;
            }
            // Single relationship to another model
            return RewriteTypeName(type);
        }
    }
}