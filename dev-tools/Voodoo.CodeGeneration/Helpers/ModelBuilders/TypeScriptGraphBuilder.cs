using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Voodoo.Messages;

namespace Voodoo.CodeGeneration.Helpers.ModelBuilders
{
    public class TypeScriptGraphBuilder : GraphBuilder<TypeScriptModelBuilder>
    {
        private IEnumerable<PropertyInfo> iResponseProperties = typeof(IResponse).GetProperties().ToArray();

        protected override void buildDeclaration(Type currentType, bool isResponse)
        {
            var nullableType = Nullable.GetUnderlyingType(currentType);
            if (currentType.IsEnum || nullableType != null && nullableType.IsEnum)
            {
                buildEnumDeclaration(currentType);
                return;
            }

            var typeName = builder.RewriteTypeName(currentType);
            if (GeneratedTypeDefinitions.Contains(currentType))
                return;

            if (GeneratedTypeNames.Contains(typeName))
                return;

            GeneratedTypeDefinitions.Add(currentType);
            GeneratedTypeNames.Add(typeName);

            var properties = currentType.GetProperties();

            output.Append($"export interface {typeName} ");
            if (isResponse && typeName != "IResponse")
            {
                output.Append("extends IResponse");
                properties = properties.Except(iResponseProperties).ToArray();
            }
            output.AppendLine(" {");
            foreach (var property in properties)
                output.AppendLine(builder.GetPropertyDeclaration(property));
            output.AppendLine("}");
        }

        protected override void buildEnumDeclaration(Type currentType)
        {
            if (GeneratedTypeDefinitions.Contains(currentType))
                return;

            GeneratedTypeDefinitions.Add(currentType);

            var nullableType = Nullable.GetUnderlyingType(currentType);
            if (nullableType != null)
            {
                currentType = nullableType;
                if (GeneratedTypeDefinitions.Contains(currentType))
                    return;

                GeneratedTypeDefinitions.Add(currentType);
            }
            var typeName = builder.RewriteTypeName(currentType);

            output.Append($"export enum {typeName} ");
            output.AppendLine(" {");
            var names = Enum.GetNames(currentType);
            foreach (var name in Enum.GetNames(currentType))
            {
                output.Append(name);
                output.Append(" = ");
                output.Append(Enum.Parse(currentType, name).To<int>());
                if (name != names.Last())
                    output.Append(",");

                output.AppendLine(string.Empty);
            }
            output.AppendLine("}");
        }
    }
}