using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Voodoo.Infrastructure.Notations;
using Voodoo.Messages;
using Voodoo.Messages.Paging;
using Voodoo.Validation;

namespace Voodoo.CodeGeneration.Helpers.ModelBuilders
{
    public class TypescriptMetadataBuilder
    {
        private string typeName;
        private PropertyInfo[] properties;
        private StringBuilder output = new StringBuilder();
        private Type[] dateTypes = new Type[] { typeof(DateTime), typeof(DateTime?) };
        private Type[] intTypes = new Type[] { typeof(short), typeof(int), typeof(long) };
        private Type[] decimalType = new Type[] { typeof(decimal) };
        private ModelBuilder modelBuilder = new TypeScriptModelBuilder();
        private Type type;

        public TypescriptMetadataBuilder(Type type, PropertyInfo[] properties)
        {
            this.type = type;
            typeName = modelBuilder.RewriteTypeName(type);
            this.properties = properties;
        }
        public string Build()
        {
            if (type.DoesImplementInterfaceOf(typeof(IResponse)))
                return string.Empty;

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

            return CodeFormatter.Format(output.ToString());
        }

        private void getDeclaration(PropertyInfo property, bool isLast)
        {
            if (property.DeclaringType == typeof(PagedResponse<>) 
                || property.DeclaringType == typeof(Response) 
                || property.DeclaringType == typeof(Response<>)
                || property.DeclaringType == typeof(PagedRequest)
                || property.DeclaringType == typeof(GridState)
                )
                return;

            var tsName = ModelBuilder.LowerCaseFirstLetter(property.Name);

            output.Append(tsName);
            output.AppendLine(":");
            output.AppendLine("{");

            var previous = false;
            previous = generateUi(property);
            previous = generateDateDeclaration(property, previous);
            previous = generateIntDeclaration(property, previous);
            previous = generateDecimalDeclaration(property, previous);
            previous = generateStringLengthDeclaration(property, previous);
            previous = generateRequiredDeclaration(property, previous);
            
            output.AppendLine("}");
            if (!isLast)
                output.Append(",");
            //TODO:
            //CollectionMustHaveAtLeastOneItem
            //CompareAttribute
            //Create GreaterThan

        }

        private bool generateUi(PropertyInfo property)
        {
            var ui = property.GetCustomAttribute<UIAttribute>();
            if (ui == null)
            {
                //TODO: if no ui attribute default format based on data type

                return true;
            }

            var items = new List<string>();
            items.Add($"displayFormat:'{ModelBuilder.LowerCaseFirstLetter(ui.DisplayFormat.ToString())}'");
            if (ui.IsHidden)
                items.Add("isHidden: true");
            if (ui.IsReadOnly)
                items.Add("isReadonly: true");
            if (!string.IsNullOrWhiteSpace(ui.Grouping))
                items.Add($"grouping:'{ui.Grouping}'");
            items.Add($"propertyName:'{property.Name}'");

            var displayName = property.GetCustomAttribute<DisplayAttribute>()?.Name ?? property.Name.ToFriendlyString();

            items.Add($"displayName:'{displayName}'");

            var lastItem = items.Last();
            foreach (var item in items)
            {
                output.Append(item);
                if (item != lastItem)
                    output.Append(",");
                output.AppendLine();            
            }
            return true;

        }

        private bool generateRequiredDeclaration(PropertyInfo property, bool hasPrevious)
        {
            if (
                property.GetCustomAttribute<RequiredAttribute>() != null ||
                property.GetCustomAttribute<EnumIsRequiredAttribute>() != null ||
                property.GetCustomAttribute<RequiredDateTimeAttribute>() != null ||
                property.GetCustomAttribute<RequiredInt>() != null ||
                property.GetCustomAttribute<RequiredNonZeroInt>() != null

                )
            {
                if (hasPrevious)
                    output.Append(",");

                output.AppendLine($"required:");
                output.AppendLine("{");
                output.AppendLine("shouldValidate:true");
                output.AppendLine("}");
                return true;
            }
            return hasPrevious;
        }

        private bool generateStringLengthDeclaration(PropertyInfo property, bool hasPrevious)
        {
            var stringLength = property.GetCustomAttribute<StringLengthAttribute>();
            if (stringLength == null)
                return hasPrevious;
            if (hasPrevious)
                output.Append(",");
            output.AppendLine($"length:");
            output.AppendLine("{");
            output.AppendLine("shouldValidate:true");

            output.Append(",");
            output.AppendLine($"min: {stringLength.MinimumLength}");

            if (stringLength.MaximumLength != 0)
            {
                output.Append(",");
                output.AppendLine($"max: {stringLength.MaximumLength}");
            }

            if (stringLength.ErrorMessage != null)
            {
                output.Append(",");
                output.AppendLine($"message: '{stringLength.ErrorMessage}'");
            }
            else
            {
                output.Append(",");
                var hasMin = stringLength.MinimumLength != 0;
                output.AppendLine(hasMin
                    ? $"message: 'must be between {stringLength.MinimumLength} and {stringLength.MaximumLength} characters'"
                    : $"message: 'no more than {stringLength.MaximumLength} characters'");
            }
            output.AppendLine("}");
            return true;

        }



        private bool generateDateDeclaration(PropertyInfo property, bool previous)
        {
            if (!dateTypes.Contains(property.PropertyType))
                return previous;

            output.AppendLine($"date:");
            output.AppendLine("{");
            output.AppendLine("shouldValidate:true");
            var range = property.GetCustomAttribute<RangeAttribute>();
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
                else
                {
                    output.Append(",");
                    output.AppendLine($"message: 'invalid date'");
                }

            }

            output.AppendLine("}");
            return true;
        }


        private bool generateIntDeclaration(PropertyInfo property, bool hasPrevious)
        {
            if (!intTypes.Contains(property.PropertyType))
                return hasPrevious;
            if (hasPrevious)
                output.Append(",");
            var requiredNonZeroInt = property.GetCustomAttribute<RequiredNonZeroInt>() != null;
            output.AppendLine($"int:");
            output.AppendLine("{");
            output.AppendLine("shouldValidate:true");
            var range = property.GetCustomAttribute<RangeAttribute>();
            if (range != null)
            {
                if (range.Minimum != null)
                {
                    output.Append(",");
                    output.AppendLine($"min: {range.Minimum}");
                }
                else if (requiredNonZeroInt)
                {
                    output.Append(",");
                    output.AppendLine($"min: 1");
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
                else
                {
                    output.Append(",");
                    output.AppendLine($"message: 'invalid whole number'");
                }

            }
            output.AppendLine("}");
            return true;
        }

        private bool generateDecimalDeclaration(PropertyInfo property, bool hasPrevious)
        {
            if (!decimalType.Contains(property.PropertyType))
                return hasPrevious;
            if (hasPrevious)
                output.Append(",");
            output.AppendLine($"decimal:");
            output.AppendLine("{");
            output.AppendLine("shouldValidate:true");
            var range = property.GetCustomAttribute<RangeAttribute>();
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
                else
                {
                    output.Append(",");
                    output.AppendLine($"message: 'invalid decimal number'");
                }
            }
            output.AppendLine("}");
            return true;
        }
    }
}
