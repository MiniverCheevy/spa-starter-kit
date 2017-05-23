using System.Data.Common;
using Core.Context;
using Core.Infrastructure;
using Effort;

namespace Tests.Fakes
{
    public class FakeContextFactory : IContextFactory
    {
        private static readonly DbConnection connection;

        static FakeContextFactory()
        {
            connection = DbConnectionFactory.CreatePersistent("this");
        }


        MainContext IContextFactory.GetContext()
        {
            return new MainContext(connection);
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