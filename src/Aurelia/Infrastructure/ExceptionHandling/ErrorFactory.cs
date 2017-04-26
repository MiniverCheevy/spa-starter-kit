using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Fernweh.Core.Context.ExceptionalContext;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http.Internal;
using Voodoo;
using Voodoo.Messages;
using HttpContext = Microsoft.AspNetCore.Http.HttpContext;
using HttpRequest = Microsoft.AspNetCore.Http.HttpRequest;

namespace Fernweh.Aurelia.Infrastructure.ExceptionHandling
{
    public class ErrorFactory
    {
        internal const string CollectionErrorKey = "CollectionFetchError";
        private Error error;
        private HttpContext context;
        private List<NameValuePair> queryString = new List<NameValuePair>();
        private List<NameValuePair> form = new List<NameValuePair>();
        private List<NameValuePair> cookies = new List<NameValuePair>();
        private List<NameValuePair> requestHeaders = new List<NameValuePair>();
        private List<NameValuePair> customData = new List<NameValuePair>();
        private DateTime creationDate;


/*
add the below line to the startup to enable reading the request
    
    app.Use((context, next) => { context.Request.EnableRewind(); return next(); });

add the below line to the startup to enable reading the form

    services.Configure<FormOptions>(options => options.BufferBody = true);
*/

        public Error GetError(Exception e, HttpContext context)
        {
            if (e == null)
                throw new ArgumentNullException(nameof(e));

            creationDate = DateTime.Now;
            this.context = context;

            buildError(e);

            foreach (var key in e.Data.Keys)
            {
                customData.Add(new NameValuePair(key.To<string>(), e.Data[key].To<string>()));
            }

            setContextProperties();
            var body = captureBody(context);
            error.ErrorHash = GetHash();
            error.FullJson = ToDetailedJson();

            return error;
        }

        private void buildError(Exception e)
        {
            var baseException = e;

            if (isBuiltInException(e))
                baseException = e.GetBaseException();

            var excptionForMessage = e;
            while (excptionForMessage.InnerException != null)
            {
                excptionForMessage = excptionForMessage.InnerException;
            }

            error = new Error
            {
                GUID = Guid.NewGuid(),
                ApplicationName = Voodoo.VoodooGlobalConfiguration.ApplicationName,
                MachineName = Environment.MachineName,
                Type = baseException.GetType().FullName,
                Message = excptionForMessage.Message,
                Source = baseException.Source,
                Details = e.ToString(),
                CreationDate = DateTime.UtcNow,
                DuplicateCount = 1
            };
        }

        private string captureBody(HttpContext context)
        {
            string body = null;
            try
            {
                context.Request.EnableRewind();
                context.Request.Body.Position = 0;

                using (var requestBodyStream = new MemoryStream())
                {
                    context.Request.Body.CopyTo(requestBodyStream);
                    requestBodyStream.Seek(0, SeekOrigin.Begin);
                    using (var reader = new StreamReader(requestBodyStream))
                    {
                        body = reader.ReadToEnd();
                        customData.Add(new NameValuePair("Request", body));
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Error parsing Request Body: " + ex.Message);
            }
            finally
            {
                context.Request.Body.Position = 0;
            }
            return body;
        }


        public int? GetHash()
        {
            if (!error.Details.HasValue()) return null;

            var result = error.Details.GetHashCode();
            return result;
        }


        private void setContextProperties()
        {
            if (context == null) return;
            var request = context.Request;
            error.User = getUser();
            error.IPAddress = this.context.Connection.RemoteIpAddress.To<string>();
            error.HTTPMethod = this.context.Request.Method;
            error.Url = this.context.Request.GetEncodedUrl();
            error.Host = this.context.Request.Host.Host.To<string>();
            Func<Func<HttpRequest, List<NameValuePair>>, List<NameValuePair>> tryGetCollection = getter =>
            {
                try
                {
                    return new List<NameValuePair>(getter(request));
                }
                catch (Exception e)
                {
                    Trace.WriteLine("Error parsing collection: " + e.Message);
                    return new List<NameValuePair> {{CollectionErrorKey, e.Message}};
                }
            };

            try
            {
                if (request.ContentType != "application/json")
                {
                    this.form = new List<NameValuePair>();
          
                    foreach (var pair in request.Form)
                    {
                        var name = pair.Key;
                        var val = pair.Value;
                        form.Add(name, val);
                    }
                }
            }
            catch (Exception e)
            {
                this.form = new List<NameValuePair>();
                form.Add("Error Reading Form", e.Message);
            }
            try
            {
                this.queryString = new List<NameValuePair>();
                foreach (var cookie in request.Query)
                {
                    var name = cookie.Key;
                    var val = cookie.Value;
                    queryString.Add(name, val);
                }
            }
            catch (Exception e)
            {
                this.queryString = new List<NameValuePair>();
                queryString.Add("Error Reading QueryString", e.Message);
            }
            try
            {
                cookies = new List<NameValuePair>();
                foreach (var cookie in request.Cookies)
                {
                    var name = cookie.Key;
                    var val = cookie.Value;
                    cookies.Add(name, val);
                }
            }
            catch (Exception e)
            {
                this.cookies = new List<NameValuePair>();
                cookies.Add("Error Reading Cookies", e.Message);
            }

            requestHeaders = new List<NameValuePair>(request.Headers.Count);
            foreach (var header in request.Headers.Keys)
            {
                // Cookies are handled above, no need to repeat
                if (string.Compare(header, "Cookie", StringComparison.OrdinalIgnoreCase) == 0)
                    continue;
                var value = request.Headers[header].To<string>();

                requestHeaders.Add(header, request.Headers[header]);
            }
        }

        private string getUser()
        {
            try
            {
                return context.User.Identity.Name;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }


        internal void AddFromData(Exception exception)
        {
            if (exception.Data.Contains("SQL"))
                error.SQL = exception.Data["SQL"] as string;


            // Regardless of what Resharper may be telling you, .Data can be null on things like a null ref exception.
            if (exception.Data != null)
            {
                if (customData == null)
                    customData = new List<NameValuePair>();

                foreach (string k in exception.Data.Keys)
                {
                    customData.Add(k, exception.Data[k].To<string>());
                }
            }
        }

        private bool isBuiltInException(Exception e)
        {
            return e.GetType().Module.ScopeName == "CommonLanguageRuntimeLibrary";
        }

        public string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }


        public string ToDetailedJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(
                new
                {
                    error.GUID,
                    error.ApplicationName,
                    CreationDate = creationDate,
                    CustomData = customData,
                    error.Details,
                    error.DuplicateCount,
                    error.ErrorHash,
                    error.HTTPMethod,
                    error.Host,
                    error.IPAddress,
                    error.IsProtected,
                    error.MachineName,
                    error.Message,
                    error.SQL,
                    error.Source,
                    error.StatusCode,
                    error.Type,
                    error.Url,
                    error.User,
                    CookieVariables = cookies,
                    RequestHeaders = requestHeaders,
                    QueryStringVariables = queryString,
                    FormVariables = form
                });
        }


        public static Error FromJson(string json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Error>(json);
        }
    }
}