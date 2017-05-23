//***************************************************************
//This code just called you a tool
//What I meant to say is that this code was generated by a tool
//so don't mess with it unless you're debugging
//subject to change without notice, might regenerate while you're reading, etc
//***************************************************************

using Core.Context.ExceptionalContext;

namespace Core.Operations.Errors.Extras
{
    public static partial class ErrorExtensions
    {
        private static ErrorMessage toErrorMessage(Error model, ErrorMessage message)
        {
            message.Id = model.Id;
            message.CreationDate = model.CreationDate;
            message.Type = model.Type;
            message.Message = model.Message;
            message.User = model.User;
            
            return message;
        }
        public static Error updateFromErrorMessage(ErrorMessage message, Error model)
        {
            model.CreationDate =message.CreationDate;
            model.Type =message.Type;
            model.Message =message.Message;
            model.User =message.User;
            return model;
        }
        private static ErrorDetail toErrorDetail(Error model, ErrorDetail message)
        {
            message.Id = model.Id;
            message.CreationDate = model.CreationDate;
            message.Type = model.Type;
            message.Host = model.Host;
            message.Url = model.Url;
            message.Message = model.Message;
            message.Details = model.Details;
            message.User = model.User;
            
            return message;
        }
        public static Error updateFromErrorDetail(ErrorDetail message, Error model)
        {
            model.CreationDate =message.CreationDate;
            model.Type =message.Type;
            model.Host =message.Host;
            model.Url =message.Url;
            model.Message =message.Message;
            model.Details =message.Details;
            model.User =message.User;
            return model;
        }           
    }
}

