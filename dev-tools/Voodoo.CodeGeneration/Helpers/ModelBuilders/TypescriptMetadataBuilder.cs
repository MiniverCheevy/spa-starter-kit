using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            output.AppendLine($"export const {typeName}Metadata =");

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
                generateDateDeclaration(property);
            else if (intTypes.Contains(property.PropertyType))
                generateIntDeclaration(property);
            else if (decimalType.Contains(property.PropertyType))
                generateDecimalDeclaration(property);

        }

        private RangeAttribute GetRangeAttribute(PropertyInfo info)
        {
            return info.GetCustomAttribute<RangeAttribute>();
        }

        private void generateDateDeclaration(PropertyInfo property)
        {
            output.AppendLine($"date:{{");
            output.AppendLine("shouldValidate:true");
            var range = GetRangeAttribute(property);
            if (range != null)
            {
                if (range.Minimum != null)
                {
                    output.Append(",");
                    output.AppendLine($"min: new Date('{range.Minimum}')");
                }
                if (range.Maximum != null)
                {
                    output.Append(",");
                    output.AppendLine($"max: new Date('{range.Maximum}')");
                }
                
                if (range.ErrorMessage != null)
                {
                    output.Append(",");
                    output.AppendLine($"message: '{range.ErrorMessage}'");
                }
                
            }
            
            output.AppendLine("}");
        }


    private void generateIntDeclaration(PropertyInfo property)
        {
            output.AppendLine($"int:{{");
            output.AppendLine("shouldValidate:true");
            var range = GetRangeAttribute(property);
            if (range != null)
            {
                if (range.Minimum != null)
                {
                    output.Append(",");
                    output.AppendLine($"min: {range.Minimum}");
                }
                if (range.Maximum != null)
                {
                    output.Append(",");
                    output.AppendLine($"max: {range.Maximum}");
                }

                if (range.ErrorMessage != null)
                {
                    output.Append(",");
                    output.AppendLine($"message: '{range.ErrorMessage}'");
                }

            }
        }

        private void generateDecimalDeclaration(PropertyInfo property)
        {
            output.AppendLine($"decimal:{{");
            output.AppendLine("shouldValidate:true");
            var range = GetRangeAttribute(property);
            if (range != null)
            {
                if (range.Minimum != null)
                {
                    output.Append(",");
                    output.AppendLine($"min: {range.Minimum}");
                }
                if (range.Maximum != null)
                {
                    output.Append(",");
                    output.AppendLine($"max: {range.Maximum}");
                }

                if (range.ErrorMessage != null)
                {
                    output.Append(",");
                    output.AppendLine($"message: '{range.ErrorMessage}'");
                }

            }
        }
    }
}
