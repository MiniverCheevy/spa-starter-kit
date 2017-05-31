using System;
using System.Threading.Tasks;
using Core.Identity;
using Core.Security;
using Microsoft.AspNetCore.Http;
using Voodoo.Logging;

namespace Fernweh.Infrastructure.Authentication
{
    public class TokenReaderMiddleware
    {
        private readonly RequestDelegate next;

        public TokenReaderMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.IsSecureRequest())
            {
                var key = "Token";
                var headers = context.Request.Headers;
                if (headers.ContainsKey(key))
                {
                    var token = headers[key].ToString();
                    try
                    {
                        var decrypted = Encryption.Decrypt<AppPrincipal>(token);
                        if (decrypted.Expiration < DateTime.Now)
                            context.Items[RequestContextProvider.AppPrincipal] = decrypted;
                    }
                    catch (Exception ex)
                    {
                        var e = new Exception($"Failed to decrypt token {token}", ex);
                        LogManager.Log(e);
                    }
                }
            }
            await next(context);
        }
    }
}