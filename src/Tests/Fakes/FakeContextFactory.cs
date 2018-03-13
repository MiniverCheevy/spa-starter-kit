using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Core.Context;
using Core.Infrastructure;
using Voodoo;

namespace Tests.Fakes
{
    public class FakeContextFactory : IContextFactory
    {
        private static readonly DbConnection connection;

        static FakeContextFactory()
        {
            
        }


        DatabaseContext IContextFactory.GetContext()
        {
            //todo: replace with ef core in memory
            return new ContextFactory().GetContext();
        }


        public string GetConnectionString()
        {
            return connection.ConnectionString;
        }
//			//DTC issues - possible fix
//		public LGMContext GetContext()
//		{

//			var context = new LGMContext();
//			//http://www.digitallycreated.net/Blog/48/entity-framework-transactionscope-and-msdtc
//#if DEBUG
//			context.Database.Connection.Open(); 
//#endif
//			return context;
//		}
    }
}