using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;

using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Core.Context.ExceptionTranslators;
using Core.Models;
using Core.Models.Identity;
using Core.Models.Logging;
using Core.Models.Scratch;
using Core.Operations.Lists;
using Microsoft.EntityFrameworkCore;
using Voodoo;

namespace Core.Context
{
    public partial class DatabaseContext : DbContext
    {
        private ListsHelper listHelper = new ListsHelper();
        public DbSet<Error> Errors { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<ApplicationSetting> ApplicationSettings { get; set; }
        
      
        private void init()
        {
            
            //Configuration.ProxyCreationEnabled = false;
            //Configuration.LazyLoadingEnabled = false;
            //Database.Log = IOC.TraceLogger.Log;
        }


        public DatabaseContext(DbContextOptions connectionString)
            : base(connectionString)
        {
            init();
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            foreach (var model in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var prop in model.GetProperties())
                {
                    if (prop.ClrType != typeof(string))
                        continue;
                    if (prop.PropertyInfo.GetCustomAttributes(false).OfType<MaxLengthAttribute>().Any())
                        continue;

                    prop.IsUnicode(false);
                    prop.SetMaxLength(128);
                }

            }

            //modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}