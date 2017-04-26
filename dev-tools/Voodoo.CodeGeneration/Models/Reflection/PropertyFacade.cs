using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Voodoo;

namespace Voodoo.CodeGeneration.Models.Reflection
{
    public class PropertyFacade
    {
        private readonly PropertyInfo info;

        public PropertyFacade(PropertyInfo info, TypeFacade parentType)
        {
            ParentType = parentType;
            ErrorMessages = new List<ErrorMessage>();
            Attributes = new List<GeneratedAttribute>();

            this.info = info;
            buildProperties();
            buildAttributes();
        }

        public PropertyGroup Group { get; set; }

        public List<ErrorMessage> ErrorMessages { get; set; }
        public List<GeneratedAttribute> Attributes { get; set; }
        public string Name { get; set; }
        public Type PropertyType { get; set; }
        public string StringifiedType { get; set; }
        public TypeFacade ParentType { get; set; }
        public string CamelCaseName { get; set; }

        public override string ToString()
        {
            return $"{Name} {PropertyType}";
        }

        private void buildProperties()
        {
            IsWritable = info.CanWrite;
            Name = info.Name;
            CamelCaseName = Name.Substring(0, 1).ToLower() + Name.Substring(1);
            PropertyType = info.PropertyType;
            StringifiedType = PropertyType.FixUpTypeName();

            if (info.PropertyType.IsScalar())
                Group = PropertyGroup.Scalar;
            else if (typeof(IEnumerable).IsAssignableFrom(info.PropertyType))
                Group = PropertyGroup.Enumerable;
            else
                Group = PropertyGroup.Navigation;
        }

        public bool IsWritable { get; set; }

        private void buildAttributes()
        {
            if (Group != PropertyGroup.Scalar)
                return;
            buildDisplayName();
            buildRequired();
            buildRange();
            buildMaxLength();
        }

        private void buildDisplayName()
        {
            if (Name == Name.ToFriendlyString())
                return;

            var name = Name;
            if (name.EndsWith("Id"))
                name = name.TrimEnd('d').TrimEnd('I');

            name = name.ToFriendlyString();

            Attributes.Add(new GeneratedAttribute {Text = $"[Display(Name = \"{name}\")]"});
        }

        private void buildRequired()
        {
            var isNullable = PropertyType.Name.ToLower().Contains("nullable");
            var isName = PropertyType == typeof(string) && Name == "Name";
            var isId = Name == "Id";
            var intTypes = new List<Type> {typeof(int), typeof(short), typeof(long)};
            var isInt = intTypes.Contains(PropertyType);

            if (!isNullable && !isName && !isId && isInt)
            {
                Attributes.Add(new GeneratedAttribute
                    {Text = "[RequiredNonZeroInt(ErrorMessage = Constants.Messages.Required)]"});
            }
            else if (!isNullable && !isName && !isId)
            {
                Attributes.Add(new GeneratedAttribute {Text = "[Required(ErrorMessage = Constants.Messages.Required)]"});
            }
        }

        private void buildRange()
        {
            if (PropertyType == typeof(DateTime) || PropertyType == typeof(DateTime?))
            {
                Attributes.Add(new GeneratedAttribute
                {
                    Text =
                        "[Range(typeof(DateTime), \"1/1/1900\", \"3/4/2050\", ErrorMessage = Constants.Messages.DateOutOfRange)]"
                });
            }
        }

        private void buildMaxLength()
        {
            if (info.PropertyType != typeof(string))
                return;

            var maxLength = info.GetCustomAttribute<MaxLengthAttribute>();
            var stringLength = info.GetCustomAttribute<StringLengthAttribute>();
            var length = 128;
            if (maxLength != null)
                length = maxLength.Length;
            if (stringLength != null)
                length = stringLength.MaximumLength;

            var errorName = $"{info.Name}TooLong";
            var attribute = string.Format("[StringLength({0}, ErrorMessage={2}Messages.{1})]", length, errorName,
                ParentType.Name);
            Attributes.Add(new GeneratedAttribute {Text = attribute});

            var message = $"public const string {errorName} = \"{length} characters or less\";";
            ErrorMessages.Add(new ErrorMessage {Text = message});
        }

        public class GeneratedAttribute
        {
            public string Text { get; set; }
        }

        public class ErrorMessage
        {
            public string Text { get; set; }
        }
    }
}