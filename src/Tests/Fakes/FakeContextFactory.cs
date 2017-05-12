using System.Data.Common;
using Effort;
using Fernweh.Core.Context;
using Fernweh.Core.Infrastructure;

namespace Fernweh.Tests.Fakes
{
    public class FakeContextFactory : IContextFactory
    {
        private static readonly DbConnection connection;

        static FakeContextFactory()
        {
            connection = DbConnectionFactory.CreatePersistent("this");
        }


        AppContext IContextFactory.GetContext()
        {
            return new AppContext(connection);
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