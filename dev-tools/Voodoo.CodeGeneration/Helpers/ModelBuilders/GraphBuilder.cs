using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Voodoo;
using Voodoo.Helpers;
using Voodoo.Messages;
using Voodoo.Messages.Paging;

namespace Voodoo.CodeGeneration.Helpers.ModelBuilders
{
    public abstract class GraphBuilder<TModelBuilder>
        where TModelBuilder : ModelBuilder, new()
    {
        protected TModelBuilder builder;
        protected readonly StringBuilder output;
        private HashSet<Type> modelTypes = new HashSet<Type>();

        protected GraphBuilder(Type[] modelTypes)
        {
            if (modelTypes != null)
            {                
                var walker = new GraphWalker(new GraphWalkerSettings { IncludeScalarTypes = false, TreatNullableTypesAsDistict = false, }, modelTypes);

                this.modelTypes = walker.GetDistinctTypes();
                var constantTypes = new Type[] 
                { typeof(IResponse),typeof(IGridState),typeof(INameIdPair), new Response().GetType(),
                    new Grouping<NameValuePair>().GetType() };
                modelTypes = constantTypes.Union(modelTypes).ToArray();
            }
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
            return response;
        }

        public void WriteModelDefinitions()
        {
            foreach (var model in modelTypes)
            {
                if (model.IsEnum)
                    buildEnumDeclaration(model);
                else
                    buildDeclaration(model, model.DoesImplementInterfaceOf(typeof(IResponse)));
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