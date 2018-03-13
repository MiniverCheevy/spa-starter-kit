using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.FileExtensions;
using Voodoo;

namespace Core.Context
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext CreateDbContext(string[] args)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            //assmume all.sln
            var directory = IoNic.PathCombineLocal(currentDirectory, @"..\React\");
            if (!Directory.Exists(directory))
            {
                //assume an actual project with only one web framework
                directory = IoNic.PathCombineLocal(currentDirectory, @"..\Web\");
            }
            Console.WriteLine($"-- using the connections string in for  in '{directory}appsettings.json', if this is incorrect edit the file DesignTimeDbContextFactory in Core");
            
            var configuration = new ConfigurationBuilder()            
                .SetBasePath(directory)
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<DatabaseContext>();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            builder.UseSqlServer(connectionString);

            return new DatabaseContext(builder.Options);
        }
    }
}
