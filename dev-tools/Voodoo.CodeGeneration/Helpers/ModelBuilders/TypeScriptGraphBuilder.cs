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

        public TypeScriptGraphBuilder(Type[] modelTypes) :base(modelTypes)
        {
            this.output.AppendLine(@"

export class DateTimeOffset
{
    constructor(value?: string | Date)
    { 

        if (value != null)
        {            
           this.internalValue = <any>new Date(<any>value).toISOString();
        }
                    
    }
    private internalValue: string;
    get() { 
        return this.internalValue;
    }
    set(value)
    { 
        this.internalValue = new Date(value).toISOString();
    }
    get date():Date {
        return new Date(this.internalValue);
    }
    set date(value:Date) {
        this.internalValue = value.toISOString(); 
    }
    //perhaps add
    //get DateDisplay
    //get TimeDisplay
    //get DateTimeDisplay
}");
        }
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

            

            buildClassDeclaration(isResponse, typeName, currentType);
            //buildConstantDeclaration(typeName, properties);

        }

        private void buildConstantDeclaration(string typeName, PropertyInfo[] properties)
        {
            output.AppendLine($"static empty()");
            output.AppendLine($"{{");
            output.AppendLine($"const result =");

            output.AppendLine(" {");
            var lastProperty = properties.Any() ? properties.Last() : null;
            foreach (var property in properties)
            {
                var isLast = property == lastProperty;
                output.AppendLine(builder.GetConstPropertyDeclaration(property, isLast));
            }
            output.AppendLine("};");
            output.AppendLine("return result;");
            output.AppendLine("}");
            output.AppendLine();
        }

        private void buildClassDeclaration(bool isResponse, string typeName, Type currentType)
        {
            var properties = currentType.GetProperties();
            output.Append($"export class {typeName} ");
            if (isResponse && typeName != "IResponse")
            {
                output.Append("extends IResponse");
                properties = properties.Except(iResponseProperties).ToArray();
            }
            output.AppendLine(" {");

            output.AppendLine("");
            buildConstantDeclaration(typeName, properties);
            output.AppendLine(new TypescriptMetadataBuilder(currentType, properties).Build());


            foreach (var property in properties)
                output.AppendLine(builder.GetPropertyDeclaration(property));



            output.AppendLine("}");
            output.AppendLine();
            
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