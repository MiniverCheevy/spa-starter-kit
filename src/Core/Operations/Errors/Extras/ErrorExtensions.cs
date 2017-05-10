using Fernweh.Core.Context;
using Fernweh.Core.Context.ExceptionalContext;

namespace Fernweh.Core.Operations.Errors.Extras
{
    public static partial class ErrorExtensions
    {
        public static ErrorRepository ErrorRepository(this FernwehContext context)
        {
            return new ErrorRepository(context);
        }

        public static ErrorMessage ToErrorMessage(this Error model)
        {
            var message = toErrorMessage(model, new ErrorMessage());
            message.CreationDate = message.CreationDate.ToLocalTime();
            return message;
        }

        public static Error UpdateFrom(this Error model, ErrorMessage message)
        {
            return updateFromErrorMessage(message, model);
        }

        public static ErrorDetail ToErrorDetail(this Error model)
        {
            var message = toErrorDetail(model, new ErrorDetail());
            return message;
        }

        public static Error UpdateFrom(this Error model, ErrorDetail message)
        {
            return updateFromErrorDetail(message, model);
        }
    }
}