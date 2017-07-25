using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Voodoo;
using Voodoo.Logging;
using Voodoo.Messages;

namespace Web.Controllers.Api
{
    [Serializable]
    public class JavascriptException : Exception
    {
        public JavascriptException()
        {
        }

        public JavascriptException(string message) : base(message)
        {
        }
    }

    public class ErrorRequest
    {
        public string ErrorMsg { get; set; }
        public string Url { get; set; }
        public string LineNumber { get; set; }
        public string Column { get; set; }
        public string ErrorObject { get; set; }
    }


    [Route("api/[controller]")]
    public class ClientErrorController : ApiControllerBase
    {
        [HttpPost]
        public async Task<Response> Post(ErrorRequest request)
        {
            return await Task.Run(() =>
                {
                    var ex = new JavascriptException($"Javascript: {request.ErrorMsg.To<string>()}");
                    if (VoodooGlobalConfiguration.ErrorDetailLoggingMethodology ==
                        ErrorDetailLoggingMethodology.LogInExceptionData)
                    {
                        ex.Data["ErrorObject"] = request.ErrorObject.To<string>();
                        ex.Data["Url"] = request.Url.To<string>();
                        ex.Data["LineNumber"] = request.LineNumber.To<string>();
                        ex.Data["Column"] = request.Column.To<string>();
                    }
                    else
                    {
                        ex =
                            new JavascriptException(
                                $"Javascript: {request.ErrorMsg.To<string>()}, {request.ErrorObject.To<string>()}, {request.Url.To<string>()}, {request.LineNumber.To<string>()}, {request.Column.To<string>()}");
                    }
                    LogManager.Log(ex);
                    return new Response();
                }
            );
        }
    }
}