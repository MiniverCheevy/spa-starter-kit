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
using Core.Context.ExceptionTranslators;
using Core.Models;
using Core.Models.Exceptions;
using Core.Models.Identity;
using Core.Models.Scratch;
using Voodoo;

namespace Core.Context
{
    public class MainContext : DbContext
    {
        private const string EffortConnectionString = "instanceid=this";
        public bool IsEffort => Database.Connection.ConnectionString == EffortConnectionString;
        public DbSet<Error> Errors { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<ApplicationSetting> ApplicationSettings { get; set; }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Team> Teams { get; set; }

        static MainContext()
        {
            // http://robsneuron.blogspot.nl/2013/11/entity-framework-upgrade-to-6.html
            var ensureDllIsCopied = SqlProviderServices.Instance;
            VoodooGlobalConfiguration.RegisterExceptionMapping<DbEntityValidationException>(
                new DbEntityValidationExceptionTranlator());
            VoodooGlobalConfiguration.RegisterExceptionMapping<SqlException>(
                new SqlExceptionTranslator());
            VoodooGlobalConfiguration.RegisterExceptionMapping<SqlException>(
                new ForiegnKeyExceptionTranslation());
            VoodooGlobalConfiguration.RegisterExceptionMapping<SqlException>(
                new UniqueConstraintExceptionTranslation());
        }

        public MainContext() : base("DefaultConnection")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
#if DEBUG
//            Database.Log = LogWrites;
#endif
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

        /// <summary>
        ///     Used to sync the many side of a one to many collection with a non nullable foreign key
        /// </summary>
        public void SyncChildren<TExisting, TModified, TKey>(List<TExisting> childCollection,
            List<TModified> modifiedCollection,
            Func<TExisting, TKey> existingKey, Func<TModified, TKey> modifiedKey,
            Func<TModified, TExisting, TExisting> transform)
            where TExisting : class, new()
        {
            var dbSet = Set<TExisting>();
            var reconciliation = childCollection.Reconcile(modifiedCollection, existingKey, modifiedKey);
            reconciliation.Deleted.ForEach(c => dbSet.Remove(c));
            reconciliation.Added.ForEach(c => childCollection.Add(transform(c, new TExisting())));
            reconciliation.Edited.ForEach(c => transform(c.Modified, c.Existing));
        }

        /// <summary>
        ///     Used to sync a collection on a database entity
        /// </summary>
        public void Sync<TExisting, TModified, TKey>(List<TExisting> childCollection,
            List<TModified> modifiedCollection,
            Func<TExisting, TKey> existingKey, Func<TModified, TKey> modifiedKey,
            Func<TModified, TExisting, TExisting> transform)
            where TExisting : class, new()
        {
            var reconciliation = childCollection.Reconcile(modifiedCollection, existingKey, modifiedKey);
            reconciliation.Deleted.ForEach(c => childCollection.Remove(c));
            reconciliation.Added.ForEach(c => childCollection.Add(transform(c, new TExisting())));
            reconciliation.Edited.ForEach(c => transform(c.Modified, c.Existing));
        }

        /// <summary>
        ///     Reconciles an arbitrary collection of entities, allows custom action on delete
        /// </summary>
        public void SyncCustom<TExisting, TModified, TKey>(List<TExisting> existingCollection,
            List<TModified> modifiedCollection,
            Func<TExisting, TKey> existingKey, Func<TModified, TKey> modifiedKey,
            Func<TModified, TExisting, TExisting> customTransform,
            Action<TModified> customAdd,
            Action<TExisting> customDelete)
            where TExisting : class, new()
        {
            var reconciliation = existingCollection.Reconcile(modifiedCollection, existingKey, modifiedKey);
            reconciliation.Deleted.ForEach(customDelete);
            reconciliation.Added.ForEach(customAdd);
            reconciliation.Edited.ForEach(c => customTransform(c.Modified, c.Existing));
        }


        private void LogWrites(string data)
        {
            Debug.WriteLine(data);
            var stackTrace = new StackTrace();
            var methodBase = stackTrace.GetFrame(3).GetMethod();
            Debug.WriteLine(methodBase.Name);
            Debug.WriteLine("\n---------------------------------------------------------------\n");
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