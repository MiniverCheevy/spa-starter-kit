using Core;
using Core.Models.Exceptions;
using Core.Context;
namespace Core.Operations.Errors.Extras
{
    public  static partial class ErrorExtensions
    {
        public static ErrorRepository ErrorRepository(this MainContext context)
        {
            return new ErrorRepository(context);
        }
        
        public static ErrorRow ToErrorRow(this Error model)
        {
            var message = toErrorRow(model, new ErrorRow());
            return message;
        }
        public static Error UpdateFrom(this  Error model, ErrorRow message)
        {
            return updateFromErrorRow(message, model);
            
        }
        public static ErrorDetail ToErrorDetail(this Error model)
        {
            var message = toErrorDetail(model, new ErrorDetail());
            return message;
        }
        public static Error UpdateFrom(this  Error model, ErrorDetail message)
        {
            return updateFromErrorDetail(message, model);
            
        }
        
    }
}

