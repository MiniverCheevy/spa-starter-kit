using Fernweh.Core.Context;
using Fernweh.Core.Infrastructure;
using System.Data.Common;
using Effort;

namespace Fernweh.Tests.Fakes
{
	public class FakeContextFactory : IContextFactory
	{
		private static readonly DbConnection connection;

		static FakeContextFactory()
		{
			connection = DbConnectionFactory.CreatePersistent("this");
		}


	    FernwehContext IContextFactory.GetContext() => new FernwehContext(connection);


        public string GetConnectionString() => connection.ConnectionString;	    

//			var context = new LGMContext();
//		{
//		public LGMContext GetContext()
//			//DTC issues - possible fix
//			//http://www.digitallycreated.net/Blog/48/entity-framework-transactionscope-and-msdtc
//#if DEBUG
//			context.Database.Connection.Open(); 
//#endif
//			return context;
//		}
	}
}