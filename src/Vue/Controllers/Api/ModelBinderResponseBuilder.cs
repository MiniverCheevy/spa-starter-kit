using Microsoft.AspNetCore.Mvc.ModelBinding;
using Voodoo.Messages;

namespace Web.Controllers.Api
{
    public class ModelBinderResponseBuilder
    {
        internal static T BuildResponse<T>(ModelStateDictionary modelState)
            where T : IResponse, new()
        {
            var response = new T {IsOk = false, Message = "Model binding error"};
            foreach (var error in modelState.Keys)
            {
                var value = modelState[error];
                foreach (var errorValue in value.Errors)
                    response.Details.Add(new NameValuePair(error, errorValue.ErrorMessage));
            }
            return response;
        }
    }
}