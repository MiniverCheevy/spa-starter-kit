using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Infrastructure;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using Core.Migrations;
using Voodoo;
using Voodoo.Logging;

namespace DbUpdate
{
    public class DatabaseUpdater
    {

        public static void UpdateDatabaseToLatestVersion(string connectionString)
        {
            var allScripts = new StringBuilder();
            var dir = IoNic.GetApplicationRootDirectory();
            try
            {

                if (connectionString == "instanceid=this")
                    return;
                updateDatabase(connectionString, ref allScripts);
                runSqlScripts(connectionString, ref allScripts);
            }
            catch (Exception ex)
            {
                try
                {

                    var file = IoNic.PathCombineLocal(dir, "DbUdate.Error.log");
                    IoNic.WriteFile(ex.ToString(), file);
                    LogManager.Log(ex);
                    Console.WriteLine(ex.ToString());
                    //patchMigrationHistory(connectionString);
                    allScripts = new StringBuilder();
                    updateDatabase(connectionString, ref allScripts);
                    runSqlScripts(connectionString, ref allScripts);
                }
                catch (Exception ex2)
                {
                    LogManager.Log(ex2);
                    Console.WriteLine(ex2.ToString());
                }
            }
            var scriptFile = IoNic.PathCombineLocal(dir, "Database.Update.sql");
            IoNic.WriteFile(allScripts.ToString(), scriptFile);
        }

        private static void runSqlScripts(string connectionString, ref StringBuilder allScripts)
        {
            var dir = IoNic.GetApplicationRootDirectory();
            var scripts = getScripts();
            foreach (var script in scripts)
            {
                try
                {
                    allScripts.AppendLine("-------------------------------------"); ;
                    allScripts.Append(script);
                    allScripts.AppendLine();
                    allScripts.AppendLine("GO");
                    allScripts.AppendLine("-------------------------------------"); ;
                    allScripts.AppendLine();
                    runscript(script, connectionString);
                }
                catch (Exception ex)
                {
                    var file = IoNic.PathCombineLocal(dir, "DbUdate.Error.log");
                    ex.Data.Add("script", script);
                    IoNic.AppendToFile(ex.ToString(), file);
                    LogManager.Log(ex);
                    Console.WriteLine(ex.ToString());
                }
            }
        }
        private static void runscript(string script, string connectionString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(script, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        private static string[] getScripts()
        {
            var script = new List<string>();
            var assembly = typeof(Configuration).Assembly;
            var resources = assembly.GetManifestResourceNames();
            foreach (var resource in resources)
            {
                if (resource.StartsWith("Core.Migrations.Scripts") || resource.EndsWith(".sql"))
                {
                    using (var stream = assembly.GetManifestResourceStream(resource))
                    {
                        using (var reader = new StreamReader(stream))
                        {
                            var result = reader.ReadToEnd();
                            var parts = result.Split(new string[] { "GO" }, StringSplitOptions.RemoveEmptyEntries);
                            script.AddRange(parts);
                        }
                    }
                }
            }
            return script.ToArray();
        }
        private static void updateDatabase(string connectionString, ref StringBuilder allScripts)
        {
            var configuration = new Core.Migrations.Configuration();
            //        connectionString, "System.Data.SqlClient")

            var migrator = new DbMigrator(configuration);
            if (migrator.GetPendingMigrations().Any())
            {
                migrator.Update();
            }
            var scriptor = new MigratorScriptingDecorator(migrator);
            var pendingMigrations = new DbMigrator(configuration).GetLocalMigrations().ToList();
            pendingMigrations.Insert(0, "0");
            var scriptMigrator = new MigratorScriptingDecorator(new DbMigrator(configuration));
            foreach (var migration in pendingMigrations.Zip(pendingMigrations.Skip(1), Tuple.Create))
            {
                var sql = scriptMigrator.ScriptUpdate(migration.Item1, migration.Item2);
                allScripts.AppendLine("-------------------------------------"); 
                allScripts.AppendLine("--Migration from " + (migration.Item1 ?? "<null> ") + " to " + (migration.Item2 ?? "<null> "));
                allScripts.AppendLine(sql);
                allScripts.AppendLine();
                allScripts.AppendLine("GO");
                allScripts.AppendLine("-------------------------------------");

            }


            allScripts.AppendLine();
            allScripts.AppendLine("/************CUSTOM SCRIPTS***************/");
            allScripts.AppendLine();
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
