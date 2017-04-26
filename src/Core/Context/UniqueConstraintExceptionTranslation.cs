using System;
using System.Linq;
using Voodoo;
using Voodoo.Infrastructure;
using Voodoo.Logging;
using Voodoo.Messages;

namespace Fernweh.Core.Context
{
    public class UniqueConstraintExceptionTranslation : ExceptionTranslation
    {
        //Cannot insert duplicate key row in object 'dbo.Users' 
        //with unique index 'IX_UserName'. The duplicate key value is (admin @admin.com).
        private const string flag = "with unique index";
        private IResponse response;

        protected override bool TranslateException(Exception exception, IResponse response)
        {
            this.response = response;
            var message = exception.Message;
            if (!message.Contains(flag))
                return false;

            return parseMessage(message);
        }

        private bool parseMessage(string message)
        {
            try
            {
                //index 'IX_UserName'.
                var start = message.IndexOf("'IX");
                var first = message.Substring(start);
                var parts = first.Split(new char[] {'\''}, StringSplitOptions.RemoveEmptyEntries);
                var indexName = parts[0].To<string>().Replace("IX_", "");
                var friendlyName = indexName.ToFriendlyString();

                var valueStart = message.LastIndexOf('(');
                var valueEnd = message.LastIndexOf(')');
                var valueFragment = message.Substring(valueStart + 1, valueEnd - valueStart - 1);

                response.Message = $"The {friendlyName} \"{valueFragment}\" is already in use. uniqueconstraint";
            }
            catch (Exception ex)
            {
                LogManager.Log(ex);
                return false;
            }

            return true;
        }
    }
}