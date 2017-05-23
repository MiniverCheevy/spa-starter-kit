using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Voodoo.CodeGeneration.Models.Reflection;
using Voodoo.CodeGeneration.Models.VisualStudio;
using Voodoo.Infrastructure.Notations;

namespace Voodoo.CodeGeneration.Helpers
{
    public class MappingFactory
    {
        private List<Mapping> mappings;
        private TypeFacade type;
        private ProjectFacade[] projects;
        private List<Type> includedTypes = new List<Type>();
        private List<string> includedTypeNames = new List<string>();

        public MappingFactory(TypeFacade type, params ProjectFacade[] projects)
        {
            this.type = type;
            this.projects = projects;
        }

        private void addMapping(TypeFacade messageType, List<GeneratedProperty> properties, string name)
        {
            if (messageType != null && includedTypes.Contains(messageType.SystemType))
                return;

            if (name != null && includedTypeNames.Contains(name))
                return;

            if (messageType == null)
                mappings.Add(new Mapping(type, properties, name));
            else
                mappings.Add(new Mapping(type, messageType, name));
            includedTypes.AddIfNotNull(messageType?.SystemType);
            includedTypeNames.AddIfNotNull(name);
        }

        public static List<Mapping> GetMappings(TypeFacade type, params ProjectFacade[] project)
        {
            return new MappingFactory(type, project).Build();
        }

        public List<Mapping> Build()
        {
            mappings = new List<Mapping>();

            var projectMappings = projects.SelectMany(c => c.MappingTypes).ToArray();

            foreach (var map in projectMappings)
            {
                var attribute = map.GetCustomAttribute<MapsToAttribute>();
                if (attribute.Type.FullName == type.SystemType.FullName)
                {
                    var facade = new TypeFacade(map);
                    mappings.Add(new Mapping(type, facade, type.Name));
                }
            }

            var messageType = Vs.Helper.FindType(type.MessageName);
            var properties = messageType == null
                ? type.MessageProperties
                : messageType.buildGeneratedProperty(messageType.Properties.ToList());
            addMapping(messageType, properties, type.MessageName);

            if (type.HasDetailFlag)
            {
                var detailType = Vs.Helper.FindType(type.DetailName);
                properties = detailType == null
                    ? type.DetailMessageProperties
                    : detailType.buildGeneratedProperty(detailType.Properties.ToList());
                addMapping(detailType, properties, type.DetailName);
            }

            return mappings;
        }

        public class Mapping
        {
            public Mapping(TypeFacade entity, TypeFacade message, string name = null)
            {
                ModelTypeName = entity.Name;
                MessageTypeName = message.Name;

                name = name ?? message.Name;
                var properties = new TypeComparer(entity, message);
                Properties = properties.ScalarProperties;
                PropertiesWithoutId = properties.ScalarPropertiesWithoutId;
                Namespace = message.SystemType.Namespace;
            }

            public Mapping(TypeFacade entity, List<GeneratedProperty> messageProperties, string name)
            {
                ModelTypeName = entity.Name;
                MessageTypeName = name;
                Properties = messageProperties.Select(c => c.Property).ToArray();
                PropertiesWithoutId = messageProperties.Select(c => c.Property).Where(c => c.Name != "Id").ToArray();
            }

            public string ModelTypeName { get; set; }
            public string MessageTypeName { get; set; }
            public string Namespace { get; set; }
            public PropertyFacade[] Properties { get; set; }
            public PropertyFacade[] PropertiesWithoutId { get; set; }
        }
    }
}