using System;
using System.Threading.Tasks;
using Core.Identity;
using Core.Security;
using Microsoft.AspNetCore.Http;

namespace Web.Infrastructure.Authentication
{
    public class TokenReaderMiddleware
    {
        private readonly RequestDelegate next;
        private const string key = "Token";
        public TokenReaderMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.IsSecureRequest())
            {
                
                
                if (!readTokenFromHeader(context));
                readTokenFromUrl(context);
            }
            await next(context);
        }
        private void readTokenFromUrl(HttpContext context)
        {
            var queryString = context.Request.Query;
            if (queryString.ContainsKey(key))
            {
                try
                {
                    var token = queryString[key].ToString();
                    var decrypted = Encryption.Decrypt<AppPrincipal>(token);
                    if (decrypted.Expiration < DateTime.Now)
                        context.Items[RequestContextProvider.AppPrincipal] = decrypted;
                }
                catch
                {
                    // ignored, token is invalid don't set the user
                    // do not log this or it will spam the error log
                }
            }
        }
        private bool readTokenFromHeader(HttpContext context)
        {
            var headers = context.Request.Headers;
            if (headers.ContainsKey(key))
            {
                try
                {
                    var token = headers[key].ToString();
                    var decrypted = Encryption.Decrypt<AppPrincipal>(token);
                    if (decrypted.Expiration < DateTime.Now)
                        context.Items[RequestContextProvider.AppPrincipal] = decrypted;
                    return true;
                }
                catch
                {
                    // ignored, token is invalid don't set the user
                    // do not log this or it will spam the error log
                }
            }
            return false;
        }
    }
}