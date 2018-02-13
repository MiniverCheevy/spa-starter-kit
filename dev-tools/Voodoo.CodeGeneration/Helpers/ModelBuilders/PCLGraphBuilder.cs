using System;
using System.Linq;
using Voodoo.Messages;

namespace Voodoo.CodeGeneration.Helpers.ModelBuilders
{
    public class PclGraphBuilder : GraphBuilder<PclModelBuilder>
    {
        public PclGraphBuilder(Type[] modelTypes):base(modelTypes)
        {
            
        }
        protected override void buildDeclaration(Type currentType, bool isResponse)
        {
            if (currentType.Namespace != null && currentType.Namespace.StartsWith("Voodoo."))
                return;

            var nullableType = Nullable.GetUnderlyingType(currentType);
            if (currentType.IsEnum || nullableType != null && nullableType.IsEnum)
            {
                buildEnumDeclaration(currentType);
                return;
            }

            if (GeneratedTypeDefinitions.Contains(currentType))
                return;

            GeneratedTypeDefinitions.Add(currentType);
            var properties = currentType.GetProperties();
            var typeName = builder.RewriteTypeName(currentType);

            output.Append($"public class {typeName} ");
            if (isResponse)
                output.Append(": Response");
            if (!isResponse && currentType.BaseType != null && currentType.BaseType.GetInterface("IGridState") != null)
                output.Append(": IGridState");

            output.AppendLine(" {");
            var responseProprties = typeof(Response).GetProperties().Select(c => c.Name).ToArray();
            foreach (var property in properties)
                if (!responseProprties.Contains(property.Name))
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

            output.Append($"public enum {typeName} ");
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