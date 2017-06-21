using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Voodoo.CodeGeneration.Helpers.ModelBuilders
{
    public class TypescriptMetadataBuilder
    {
        private string typeName;
        private PropertyInfo[] properties;
        private StringBuilder output = new StringBuilder();

        public TypescriptMetadataBuilder(string typeName, PropertyInfo[] properties)
        {
            this.typeName = typeName;
            this.properties = properties;
        }
        public string Build()
        {
            output.AppendLine($"export const Empty{typeName}Metadata =");

            output.AppendLine(" {");
            var lastProperty = properties.Any() ? properties.Last() : null;
            foreach (var property in properties)
            {
                var isLast = property == lastProperty;
                getDeclaration(property, isLast);
            }
            output.AppendLine("}");
            output.AppendLine();
            return output.ToString();
        }

        private void getDeclaration(PropertyInfo property, bool isLast)
        {
            var dateTypes = new Type[] { typeof(DateTime), typeof(DateTime?) };
            var intTypes = new Type[] { typeof(Int16), typeof(Int32), typeof(Int64) };
            var decimalType = new Type[] { typeof(decimal) };

            if (dateTypes.Contains(property.PropertyType))
                generateDateDeclaration();
            else if (intTypes.Contains(property.PropertyType))
                generateIntDeclaration();
            else if (decimalType.Contains(property.PropertyType))
                generateDecimalDeclaration();

        }

        private void generateDateDeclaration()
        {
            throw new NotImplementedException();
        }

        private void generateIntDeclaration()
        {
            throw new NotImplementedException();
        }

        private void generateDecimalDeclaration()
        {
            throw new NotImplementedException();
        }
    }
}
