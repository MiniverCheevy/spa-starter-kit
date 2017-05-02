using System;
using System.Collections.Generic;
using System.Data.Entity.Design.PluralizationServices;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Voodoo.CodeGeneration.Helpers;
using Voodoo.Infrastructure.Notations;

namespace Voodoo.CodeGeneration.Models.Reflection
{
    public class TypeFacade
    {
        public TypeFacade(Type type)
        {
            SystemType = type;
            createNames(type.Name);
            buildProperties();
            Namespace = type.Namespace;
        }


        private void createNames(string name)
        {
            Name = name;
            var pluralizer = PluralizationService.CreateService(CultureInfo.CurrentCulture);
            PluralName = pluralizer.Pluralize(Name);
            CamelCaseName = Name.Substring(0, 1).ToLower() + Name.Substring(1);
            MessageName = $"{Name}Message";
            DetailName = $"{Name}Detail";
            buildProperties();
        }

        public bool IsFake { get; set; }
        public string Namespace { get; set; }
        public string DetailName { get; set; }
        public string MessageName { get; set; }
        public string Name { get; set; }
        public string PluralName { get; set; }
        public string CamelCaseName { get; set; }
        public bool HasId { get; set; }
        public bool HasDetailFlag { get; set; }
        public bool HasActiveFlag { get; set; }
        public bool HasSortOrder { get; set; }
        public PropertyFacade[] Properties { get; set; }
        public Type SystemType { get; set; }
        public bool HasName { get; set; }
        public List<GeneratedProperty> MessageProperties { get; set; }
        public List<GeneratedProperty> DetailMessageProperties { get; set; }
        public string DetailQueryMapMethodName { get; set; }
        public string DetailQueryMessageName { get; set; }

        public override string ToString()
        {
            return SystemType.FullName;
        }

        private void buildProperties()
        {
            Properties = SystemType.GetProperties()
                .Where(c => CustomAttributeExtensions.GetCustomAttribute<SecretAttribute>((MemberInfo) c) == null)
                .Select(c => new PropertyFacade(c, this))
                .ToArray();

            if (Properties.Any(c => c.Name == "IsActive"))
                HasActiveFlag = true;
            if (Properties.Any(c => c.Name == "SortOrder"))
                HasSortOrder = true;
            if (Properties.Count() > 5 || Vs.Helper.FindType(DetailName) != null)
                HasDetailFlag = true;

            DetailQueryMessageName = HasDetailFlag ? DetailName : MessageName;
            DetailQueryMapMethodName = HasDetailFlag ? "ToDetail" : "ToMessage";
            if (Properties.Any(c => c.Name == "Id" && c.PropertyType == typeof(int)))
                HasId = true;
            if (HasId && Properties.Any(c => c.Name == "Name"))
                HasName = true;

            buildMessageProperties();
            buildDetailMessageProperties();
        }

        private void buildMessageProperties()
        {
            const int threshold = 5;
            var items = new List<PropertyFacade>();
            foreach (var property in Properties.Where(c => c.Group != PropertyGroup.Enumerable && c.IsWritable).ToArray())
            {
                if (items.Count < threshold)
                    items.Add(property);
                else
                    continue;
            }

            MessageProperties = buildGeneratedProperty(items);
        }

        public List<GeneratedProperty> buildGeneratedProperty(List<PropertyFacade> items)
        {
            var properties = new List<GeneratedProperty>();
            foreach (var item in items.Where(c=>c.IsWritable).ToArray())
            {
                if (item.Group != PropertyGroup.Scalar)
                    continue;
                var prop = new GeneratedProperty
                {
                    ParentType = this,
                    PathToObject = $".{item.Name}",
                    Property = item,
                    PropertyName = item.Name,
                    StringifiedTypeName = item.PropertyType.FixUpTypeName()
                };
                properties.Add(prop);
            }
            return properties;
        }

        private void buildDetailMessageProperties()
        {
            DetailMessageProperties = buildGeneratedProperty(Properties.ToList());
        }

        public static TypeFacade CreateEmptyType(string targetTypeName)
        {
            var typeFacade = new TypeFacade(typeof(System.Empty));
            typeFacade.createNames(targetTypeName);
            typeFacade.Namespace = "System";
            typeFacade.IsFake = true;
            return typeFacade;
        }
    }
}