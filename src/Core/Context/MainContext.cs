using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.SqlServer;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Core.Context.ExceptionTranslators;
using Core.Models;
using Core.Models.Exceptions;
using Core.Models.Identity;
using Core.Models.Scratch;
using Core.Operations.Lists;
using Voodoo;

namespace Core.Context
{
    public partial class MainContext : DbContext
    {
        private ListsHelper listHelper = new ListsHelper();
        private const string EffortConnectionString = "instanceid=this";
        public bool IsEffort => Database.Connection.ConnectionString == EffortConnectionString;
        public DbSet<Error> Errors { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<ApplicationSetting> ApplicationSettings { get; set; }
        
        public MainContext() : base("DefaultConnection")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        public MainContext(string connectionString)
            : base(connectionString)
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        public MainContext(DbConnection connection)
            : base(connection, true)
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<string>()
                .Where(c => !c.GetCustomAttributes(false).OfType<MaxLengthAttribute>().Any())
                .Configure(p => p.HasMaxLength(128));
            modelBuilder.Properties()
                .Where(c => c.PropertyType == typeof(string))
                .Where(c => c.Name != "Id")
                .Configure(p => p.IsUnicode(false));

            

            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}