using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voodoo;

namespace DbUpdate
{
    public static class Program
    {
        public static void Main(string[] args)
        {

            var connectionString = args.FirstOrDefault().To<string>();
            
            connectionString = Objectifyer.Base64Decode(connectionString);
            
            DatabaseUpdater.UpdateDatabaseToLatestVersion(connectionString);
        }
    }
}
