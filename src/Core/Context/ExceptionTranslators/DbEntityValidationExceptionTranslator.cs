using System;
using System.Data.Entity.Validation;
using Voodoo;
using Voodoo.Infrastructure;
using Voodoo.Messages;

namespace Fernweh.Core.Context.ExceptionTranslators
{
    public class DbEntityValidationExceptionTranlator : ExceptionTranslation
    {
        protected override bool TranslateException(Exception exception, IResponse response)
        {
            var dbException = exception as DbEntityValidationException;
            if (dbException == null)
                return false;
            response.Message = dbException.Message;
            foreach (var item in dbException.EntityValidationErrors)
            foreach (var error in item.ValidationErrors)
                response.Details.Add(error.PropertyName, error.ErrorMessage);
            return true;
        }
    }
}