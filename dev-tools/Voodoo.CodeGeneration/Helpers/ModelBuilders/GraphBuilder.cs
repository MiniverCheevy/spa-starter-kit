using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Voodoo;
using Voodoo.Messages;

namespace Voodoo.CodeGeneration.Helpers.ModelBuilders
{
    public abstract class GraphBuilder<TModelBuilder>
        where TModelBuilder : ModelBuilder, new()
    {
        private HashSet<string> generatedNames = new HashSet<string>();
        private HashSet<Type> alreadyTouched = new HashSet<Type>();
        protected TModelBuilder builder;
        protected readonly StringBuilder output;

        protected GraphBuilder()
        {
            output = new StringBuilder();
            builder = new TModelBuilder();
            var constantTypes = new Type[] { typeof(IResponse), new Response().GetType(), new Grouping<NameValuePair>().GetType() };
            AddTypes(constantTypes);
        }

        protected List<Type> GeneratedTypeDefinitions { get; set; } = new List<Type>();
        protected List<string> GeneratedTypeNames { get; set; } = new List<string>();

        public ServiceDeclaration AddTypes(Type requestType, Type responseType)
        {
            var response = new ServiceDeclaration
            {
                ResponseDeclaration = builder.RewriteTypeName(responseType),
                RequestDeclaration = builder.RewriteTypeName(requestType)
            };
            buildGraph(requestType, false);
            buildGraph(responseType, true);
            return response;
        }

        public void AddTypes(Type[] types)
        {
            var modelTypes = types.Distinct().OrderBy(c => c.Name).ToArray();
            foreach (var model in modelTypes)
            {
                buildGraph(model, model.DoesImplementInterfaceOf(typeof(IResponse)));
            }
        }

        private void buildGraph(Type type, bool isResponse)
        {
            if (!shouldWrite(type))
                return;

            buildDeclaration(type, isResponse);
            
            //var metaData = new TypescriptMetadataBuilder(type, type.GetProperties());
            //output.AppendLine(metaData.Build());
            var types = new List<Type>();
            var enumTypes = new List<Type>();
            types.AddRange(builder.getComplexPropertyTypes(type));
            enumTypes.AddRange(builder.getEnumPropertyTypes(type));
            foreach (var t in types)
            {
                if (!shouldWrite(t))
                    continue;

                //var childMetaData = new TypescriptMetadataBuilder(t, t.GetProperties());
                //output.AppendLine(childMetaData.Build());
                buildDeclaration(t, false);
                enumTypes.AddRange(builder.getEnumPropertyTypes(t));
                buildGraph(t, false);
            }

            foreach (var t in enumTypes.Distinct().ToArray())
            {
                if (!alreadyTouched.Contains(t))
                {
                    alreadyTouched.Add(t);
                    buildEnumDeclaration(t);
                }

            }
        }
        private bool shouldWrite(Type type)
        {
            if (alreadyTouched.Contains(type))
                return false;
            alreadyTouched.Add(type);
            var name = builder.RewriteTypeName(type);
            if (generatedNames.Contains(name))
                return false;
            generatedNames.Add(name);
            return true;
        }
        public string GetOutput()
        {
            return output.ToString();
        }

        protected abstract void buildDeclaration(Type currentType, bool isResponse);
        protected abstract void buildEnumDeclaration(Type currentType);
    }
}