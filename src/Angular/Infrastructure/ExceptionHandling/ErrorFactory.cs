﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Core;
using Core.Infrastructure.Logging;
using Core.Models.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http.Internal;
using Newtonsoft.Json;
using Voodoo;
using Voodoo.Messages;

namespace Web.Infrastructure.ExceptionHandling
{
    public class ErrorFactory : IErrorFactory
    {
        internal const string CollectionErrorKey = "CollectionFetchError";
        private Exception excpetion;
        private HttpContext context;
        private List<NameValuePair> cookies = new List<NameValuePair>();
        private DateTime creationDate;
        private List<NameValuePair> customData = new List<NameValuePair>();
        private Error error;
        private List<NameValuePair> form = new List<NameValuePair>();
        private List<NameValuePair> queryString = new List<NameValuePair>();
        private List<NameValuePair> requestHeaders = new List<NameValuePair>();

        public ErrorFactory(Exception excpetion, HttpContext context)
        {
            this.excpetion = excpetion;
            this.context = context;
        }
/*
add the below line to the startup to enable reading the request
    
    app.Use((context, next) => { context.Request.EnableRewind(); return next(); });

add the below line to the startup to enable reading the form

    services.Configure<FormOptions>(options => options.BufferBody = true);
*/

        public Error GetError()
        {
            var start = DateTime.UtcNow;
            if (excpetion == null)
                throw new ArgumentNullException(nameof(excpetion));

            creationDate = DateTime.Now;

            buildError(excpetion);

            foreach (var key in excpetion.Data.Keys)
                customData.Add(new NameValuePair(key.To<string>(), excpetion.Data[key].To<string>()));

            setContextProperties();
            var body = captureBody(context);
            error.ErrorHash = GetHash();
            error.FullJson = ToDetailedJson();

            var duration = DateTime.UtcNow.Subtract(start);

            return error;
        }

        private void buildError(Exception e)
        {
            var baseException = e;

            if (isBuiltInException(e))
                baseException = e.GetBaseException();

            var excptionForMessage = e;
            while (excptionForMessage.InnerException != null)
                excptionForMessage = excptionForMessage.InnerException;

            error = new Error
            {
                MachineName = Environment.MachineName,
                Type = baseException.GetType().FullName,
                Message = excptionForMessage.Message,
                Source = baseException.Source,
                Details = e.ToString(),
                CreationDate = DateTime.UtcNow
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


        [DebuggerNonUserCode]
        private void setContextProperties()
        {
            if (context == null) return;
            var request = context.Request;
            error.User = getUser();
            error.IpAddress = context.Connection.RemoteIpAddress.To<string>();
            error.HttpMethod = context.Request.Method;
            error.Url = context.Request.GetEncodedUrl();
            error.Host = context.Request.Host.Host.To<string>();
            error.RequestId = IOC.RequestContext?.Id;

            Func<Func<HttpRequest, List<NameValuePair>>, List<NameValuePair>> tryGetCollection = getter =>
            {
                try
                {
                    return new List<NameValuePair>(getter(request));
                }
                catch (Exception e)
                {
                    IOC.TraceLogger.Log("Error parsing collection: " + e.Message);
                    return new List<NameValuePair> {{CollectionErrorKey, e.Message}};
                }
            };

            try
            {
                if (request.ContentType != "application/json")
                {
                    form = new List<NameValuePair>();

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
                form = new List<NameValuePair> {{"Error Reading Form", e.Message}};
            }
            try
            {
                queryString = new List<NameValuePair>();
                foreach (var cookie in request.Query)
                {
                    var name = cookie.Key;
                    var val = cookie.Value;
                    queryString.Add(name, val);
                }
            }
            catch (Exception e)
            {
                queryString = new List<NameValuePair> {{"Error Reading QueryString", e.Message}};
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
                cookies = new List<NameValuePair> {{"Error Reading Cookies", e.Message}};
            }

            requestHeaders = new List<NameValuePair>(request.Headers.Count);
            foreach (var header in request.Headers.Keys)
            {
                // Cookies are handled above, no need to repeat
                if (string.Compare(header, "Cookie", StringComparison.OrdinalIgnoreCase) == 0)
                    continue;
                var value = request.Headers[header].To<string>();
                requestHeaders.Add(header, value);
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

            // Regardless of what Resharper may be telling you, .Data can be null on things like a null ref exception.
            if (exception.Data != null)
            {
                if (customData == null)
                    customData = new List<NameValuePair>();

                foreach (string k in exception.Data.Keys)
                    customData.Add(k, exception.Data[k].To<string>());
            }
        }

        private bool isBuiltInException(Exception e)
        {
            return e.GetType().Module.ScopeName == "CommonLanguageRuntimeLibrary";
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }


        public string ToDetailedJson()
        {
            return JsonConvert.SerializeObject(
                new
                {
                    CreationDate = creationDate,
                    CustomData = customData,
                    error.Details,
                    error.ErrorHash,
                    HTTPMethod = error.HttpMethod,
                    error.Host,
                    IPAddress = error.IpAddress,
                    error.MachineName,
                    error.Message,
                    error.Source,
                    error.StatusCode,
                    error.Type,
                    error.Url,
                    error.User,
                    CookieVariables = cookies,
                    RequestHeaders = requestHeaders,
                    QueryStringVariables = queryString,
                    FormVariables = form,
                    TraceLogs = IOC.TraceLogger.GetAllLogs(true)
                });
        }


        public static Error FromJson(string json)
        {
            return JsonConvert.DeserializeObject<Error>(json);
        }
    }
}