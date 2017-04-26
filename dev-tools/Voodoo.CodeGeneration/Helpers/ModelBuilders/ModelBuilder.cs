using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Voodoo;

namespace Voodoo.CodeGeneration.Helpers.ModelBuilders
{
    public abstract class ModelBuilder
    {
        public abstract string RewriteTypeName(Type type);
        public abstract string GenerateDeclaration(Type modelType, params string[] exclusions);
        public abstract string GetPropertyDeclaration(PropertyInfo property);

        public static string LowerCaseFirstLetter(string @string)
        {
            @string = @string.To<string>();
            if (@string.Length < 2)
                return @string;
            return $"{@string.Substring(0, 1).ToLower()}{@string.Substring(1)}";
        }

        public Type[] getComplexPropertyTypes(Type type)
        {
            var result = new List<Type>();
            getComplexPropertyTypes(type, ref result);
            return result.Distinct().Where(c => !c.IsScalar()).ToArray();
        }

        public Type[] getEnumPropertyTypes(Type type)
        {
            var result = new List<Type>();
            getEnumPropertyTypes(type, ref result);
            return result.Distinct().ToArray();
        }

        private void getComplexPropertyTypes(Type type, ref List<Type> result)
        {
            if (result.Contains(type))
                return;
            foreach (var property in type.GetProperties())
            {
                var family = GetTypeFamily(property.PropertyType);

                if (family == TypeFamily.Model)
                {
                    if (!result.Contains(property.PropertyType))
                    {
                        result.Add(property.PropertyType);
                        getComplexPropertyTypes(property.PropertyType, ref result);
                    }
                }
                else if (family == TypeFamily.Collection)
                {
                    if (property.PropertyType.GetGenericArguments().Any())
                    {
                        var collectionType = property.PropertyType.GetGenericArguments().First();
                        if (!result.Contains(collectionType))
                        {
                            result.Add(collectionType);
                            getComplexPropertyTypes(collectionType, ref result);
                        }
                    }
                }
            }
        }

        private void getEnumPropertyTypes(Type type, ref List<Type> result)
        {
            foreach (var property in type.GetProperties())
            {
                var family = GetTypeFamily(property.PropertyType);

                if (family == TypeFamily.Enum && !result.Contains(property.PropertyType))
                    result.Add(property.PropertyType);
            }
        }

        protected TypeFamily GetTypeFamily(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            var nullableUnderlyingType = Nullable.GetUnderlyingType(type);
            var isString = (type == typeof(string));
            var isEnumerable = typeof(IEnumerable).IsAssignableFrom(type);
            var isDictionary = type.FullName.StartsWith(typeof(IDictionary).FullName)
                               || type.FullName.StartsWith(typeof(IDictionary<,>).FullName)
                               || type.FullName.StartsWith(typeof(Dictionary<,>).FullName);
            var isClr = (type.Module.ScopeName == "CommonLanguageRuntimeLibrary");

            if (!isString && !isDictionary && isEnumerable)
                return TypeFamily.Collection;
            else if (type.IsEnum || nullableUnderlyingType != null && nullableUnderlyingType.IsEnum)
                return TypeFamily.Enum;
            else if (type.Module.ScopeName == "CommonLanguageRuntimeLibrary")
                return TypeFamily.System;
            else
                return TypeFamily.Model;
        }

        protected enum TypeFamily
        {
            System = 1,
            Model,
            Collection,
            Enum
        }
    }
}