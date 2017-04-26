using System;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using Fernweh.Core.Migrations;
using Voodoo;
using Voodoo.Logging;

namespace DbUpdate
{
    public class DatabaseUpdater
    {

        public static void UpdateDatabaseToLatestVersion(string connectionString)
        {
            
            try
            {
                updateDatabase(connectionString);
            }
            catch (Exception ex)
            {
                try
                {
                    var dir = IoNic.GetApplicationRootDirectory();
                    var file = IoNic.PathCombineLocal(dir, "DbUdate.Error.log");
                    IoNic.WriteFile(ex.ToString(), file);
                    LogManager.Log(ex);
                    Console.WriteLine(ex.ToString());
                    patchMigrationHistory(connectionString);
                    updateDatabase(connectionString);
                }
                catch (Exception ex2)
                {
                    LogManager.Log(ex2);
                    Console.WriteLine(ex2.ToString());
                }
            }
        }

        private static void updateDatabase(string connectionString)
        {
            var configuration = new Configuration();
            if (connectionString == "instanceid=this")
                return;
            configuration.TargetDatabase = new DbConnectionInfo(
                connectionString, "System.Data.SqlClient");

            var migrator = new DbMigrator(configuration);
            var migrations = migrator.GetPendingMigrations();
            migrator.Update();
        }
        private static void patchMigrationHistory(string connectionString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand("ALTER TABLE __MigrationHistory CreatedOnDate NULL", connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

    }
}
