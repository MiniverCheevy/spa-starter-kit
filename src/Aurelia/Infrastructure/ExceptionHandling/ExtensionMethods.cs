//from https://github.com/NickCraver/StackExchange.Exceptional

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text.RegularExpressions;
using Voodoo.Messages;

namespace Fernweh.Aurelia.Infrastructure.ExceptionHandling
{
    public static class ExtensionMethods
    {
        public static void Add(this List<NameValuePair> list, string name, string value)
        {
            list.Add(new NameValuePair {Name = name, Value = value});
        }

      
        public static string Truncate(this string s, int maxLength)
        {
            return (s.HasValue() && s.Length > maxLength) ? s.Remove(maxLength) : s;
        }

        public static string TruncateWithEllipsis(this string s, int maxLength)
        {
            const string ellipsis = "...";
            return (s.HasValue() && s.Length > maxLength) ? (s.Truncate(maxLength - ellipsis.Length) + ellipsis) : s;
        }

        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }

        public static string IsNullOrEmptyReturn(this string s, params string[] otherPossibleResults)
        {
            if (s.HasValue())
                return s;

            foreach (string t in otherPossibleResults ?? new string[0])
            {
                if (t.HasValue())
                    return t;
            }

            return "";
        }

        public static bool HasValue(this string s)
        {
            return !IsNullOrEmpty(s);
        }

        public const string UnknownIP = "0.0.0.0";

        private static readonly Regex IPv4Regex = new Regex(@"\b([0-9]{1,3}\.){3}[0-9]{1,3}$",
            RegexOptions.Compiled | RegexOptions.ExplicitCapture);

        private static bool IsPrivateIP(string s)
        {
            return (s.StartsWith("192.168.") || s.StartsWith("10.") || s.StartsWith("127.0.0."));
        }

        public static string GetRemoteIP(this NameValueCollection serverVariables)
        {
            var ip = serverVariables["REMOTE_ADDR"]; // could be a proxy -- beware
            var ipForwarded = serverVariables["HTTP_X_FORWARDED_FOR"];

            // check if we were forwarded from a proxy
            if (ipForwarded.HasValue())
            {
                ipForwarded = IPv4Regex.Match(ipForwarded).Value;
                if (ipForwarded.HasValue() && !IsPrivateIP(ipForwarded))
                    ip = ipForwarded;
            }

            return ip.HasValue() ? ip : UnknownIP;
        }

      
        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0);

        public static long ToEpochTime(this DateTime dt)
        {
            return (long) (dt - Epoch).TotalSeconds;
        }

        public static long? ToEpochTime(this DateTime? dt)
        {
            return dt.HasValue ? (long?) ToEpochTime(dt.Value) : null;
        }
    
        public static NameValueCollection ToNameValueCollection(this Dictionary<string, string> dict)
        {
            if (dict == null) return new NameValueCollection();

            var result = new NameValueCollection(dict.Count);
            foreach (var kvp in dict)
            {
                result.Add(kvp.Key, kvp.Value);
            }
            return result;
        }

      
    }
}