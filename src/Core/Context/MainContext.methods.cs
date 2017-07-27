using Core.Operations.Lists;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Context.ExceptionTranslators;
using Voodoo;

namespace Core.Context
{
    public partial class MainContext
    {
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
        public async Task<List<IListItem>> GetList(Lists list, bool includeInactive = false)
        {
            return await listHelper.GetList(this, list, includeInactive);
        }
        public async Task<ListsResponse> GetLists(params Lists[] lists)
        {
            var request = new ListsRequest { Lists = lists.ToList() };
            return await listHelper.GetLists(this, request);
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
    }
}
