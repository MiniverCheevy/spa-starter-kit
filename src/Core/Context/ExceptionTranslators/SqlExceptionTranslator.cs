using System;
using System.Data.SqlClient;
using Voodoo.Infrastructure;
using Voodoo.Messages;

namespace Core.Context.ExceptionTranslators
{
    public class SqlExceptionTranslator : ExceptionTranslation
    {
        protected override bool TranslateException(Exception exception, IResponse response)
        {
            var ex = response.Exception ?? exception;

            var se = exception as SqlException;
            if (se != null)
            {
                ex.Data.Add("SQL-Server", se.Server);
                ex.Data.Add("SQL-ErrorNumber", se.Number.ToString());
                ex.Data.Add("SQL-LineNumber", se.LineNumber.ToString());
                if (!string.IsNullOrWhiteSpace(se.Procedure))
                    ex.Data.Add("SQL-Procedure", se.Procedure);
                var counter = 0;
                foreach (var item in se.Errors)
                {
                    counter++;
                    ex.Data.Add($"Error{counter}", item);
                }
            }
            return false;
        }
    }
}