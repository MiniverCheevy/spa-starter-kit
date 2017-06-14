using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using Core.Operations.Errors.Extras;
using Voodoo;
using Voodoo.Messages;

namespace Core.Operations.Errors
{
    public static class ErrorJsonDeserializer
    {
        public const string CustomData = "Custom Data";
        public const string ServerData = "Server Data";
        public const string QueryString = "Query String";
        public const string Form = "Form";
        public const string Headers = "Headers";

        private static string decodeUnicode(object val)
        {
            var value = val.To<string>();
            return Regex.Replace(
                value,
                @"\\u(?<Value>[a-zA-Z0-9]{4})",
                m =>
                {
                    return ((char) int.Parse(m.Groups["Value"].Value,
                        NumberStyles.HexNumber)).ToString();
                });
        }

        public static ErrorDetail Deserialize(string json)
        {
            var response = new ErrorDetail();
            var error = new JavaScriptSerializer().Deserialize(json, typeof(ErrorModel)).To<ErrorModel>();
            //JsonConvert.DeserializeObject<ErrorModel>(json);

            response.Type = error.Type;
            response.Message = error.Message;
            response.Details = decodeUnicode(error.Details);
            response.Host = error.Host;
            var details = new List<NameValuePair>
            {
                new NameValuePair("Url", error.Url),
                new NameValuePair("HTTP Method", error.HTTPMethod),
                new NameValuePair("User", error.User),
                new NameValuePair("Host", error.Host),
                new NameValuePair("IP Address", error.IPAddress),
                new NameValuePair("Source", error.Source)
            };

            response.Items.Add(new Grouping<NameValuePair> {Name = "Details", Data = details});
            response.Items.Add(new Grouping<NameValuePair> {Name = CustomData, Data = error.CustomData});
            response.Items.Add(new Grouping<NameValuePair> {Name = QueryString, Data = error.QueryString});
            response.Items.Add(new Grouping<NameValuePair> {Name = Form, Data = error.Form});
            response.Items.Add(new Grouping<NameValuePair> {Name = Headers, Data = error.RequestHeaders});

            return response;
        }
    }
}