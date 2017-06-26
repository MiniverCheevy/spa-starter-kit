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
        private List<Type> alreadyTouched = new List<Type>();
        protected TModelBuilder builder;
        protected readonly StringBuilder output;

        protected GraphBuilder()
        {
            output = new StringBuilder();
            builder = new TModelBuilder();
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
            if (alreadyTouched.Contains(type))
                return;
            alreadyTouched.Add(type);
            buildDeclaration(type, isResponse);

            var types = new List<Type>();
            var enumTypes = new List<Type>();
            types.AddRange(builder.getComplexPropertyTypes(type));
            enumTypes.AddRange(builder.getEnumPropertyTypes(type));
            foreach (var t in types)
            {
                if (!alreadyTouched.Contains(t))
                {
                    alreadyTouched.Add(t);
                    buildDeclaration(t, false);
                    enumTypes.AddRange(builder.getEnumPropertyTypes(t));
                    buildGraph(t, false);
                }
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

        public string GetOutput()
        {
            return output.ToString();
        }

        protected abstract void buildDeclaration(Type currentType, bool isResponse);
        protected abstract void buildEnumDeclaration(Type currentType);
    }
}