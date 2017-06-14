using System;
using System.Data.Entity.Design.PluralizationServices;
using System.Globalization;
using System.Linq;
using Voodoo.Infrastructure;
using Voodoo.Logging;
using Voodoo.Messages;

namespace Core.Context.ExceptionTranslators
{
    public class ForiegnKeyExceptionTranslation : ExceptionTranslation
    {
        //The INSERT statement conflicted with the FOREIGN KEY constraint "FK_dbo.Evaluations_dbo.Users_UserId". The conflict occurred in database "LGM", table "dbo.Users", column 'Id'.
        //The statement has been terminated.

        private const string flag = "FOREIGN KEY constraint";
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
                //"FK_dbo.Evaluations_dbo.Users_UserId".

                var first = message.Split('"');
                if (first.Length < 2)
                    return false;

                var second = first[1].Split('_');
                var leftTable = second[1];
                var rightTable = second[2];

                var schemalessLeftTable = leftTable.Split('.').Last();
                var schemalessRightTable = rightTable.Split('.').Last();

                var pluralizer = PluralizationService.CreateService(CultureInfo.CurrentCulture);

                var singularLeftTable = pluralizer.Singularize(schemalessLeftTable);
                var singularRightTable = pluralizer.Singularize(schemalessRightTable);

                response.Message = $"The {singularLeftTable} has a(n) invalid {singularRightTable}. fkconstraint";
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