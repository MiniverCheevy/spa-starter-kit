using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Voodoo.CodeGeneration.Models;
using Voodoo.CodeGeneration.Models.Reflection;
using Voodoo.CodeGeneration.Pluralizer;

namespace Voodoo.CodeGeneration.Helpers
{
    public class NameValuePairBuilder
    {
        private readonly Type contextType;
        private PluralizationService pluralizer = PluralizationService.CreateService(CultureInfo.CurrentCulture);
        private List<string> lists = new List<string>();
        public NameValuePairBuilder(Type contextType)
        {
            this.contextType = contextType;
        }

        public NameValuePairTypeInformation[] Build()
        {
            var data = buildEnums();
            data.AddRange(buildDbSets());

            return data.ToArray();
        }

        private List<NameValuePairTypeInformation> buildEnums()
        {
            var types = contextType.Assembly.GetTypesSafetly();
            var enumTypes = types.Where(c => c.IsEnum);

            var response =
                enumTypes.Select(
                        c =>
                            new NameValuePairTypeInformation
                            {
                                EntityType = c,
                                IsEnum = true,
                                Name = c.Name,
                                PluralName = pluralizer.Pluralize(c.Name)
                            })
                    .ToList();
            lists = response.Select(c => c.PluralName).ToList();
            var skipped = response.Where(c => c.EntityType.FullName.Contains("+")).ToArray();
            skipped.ForEach(
                c =>
                    Vs.Helper.Log.Add(LogEntry.Error("Enums inside of classes not supported: {0}",
                        c.EntityType.FullName)));

            return response.Where(c => !c.EntityType.FullName.Contains("+")).ToList();
        }

        private List<NameValuePairTypeInformation> buildDbSets()
        {
            var dbSets =
                contextType.GetProperties().Where(c => c.PropertyType.IsGenericType &&
                                                       c.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>))
                    .ToArray()
                    .Select(c => new NameValuePairTypeInformation
                    {
                        DbSet = c,
                        EntityType = c.PropertyType.GetGenericArguments().FirstOrDefault()
                    }).Where(c => c.EntityType != null)
                    .ToArray();

            dbSets = dbSets.Select(c => new NameValuePairTypeInformation
                {
                    DbSet = c.DbSet,
                    EntityType = c.EntityType,
                    Facade = new TypeFacade(c.EntityType),
                    Name = c.EntityType.Name,
                    PluralName = pluralizer.Pluralize(c.EntityType.Name)
                }).ToArray()
                .Where(c => c.Facade.HasName && c.Facade.HasId && !lists.Contains(c.PluralName)).ToArray();
            return dbSets.ToList();
        }
    }
}