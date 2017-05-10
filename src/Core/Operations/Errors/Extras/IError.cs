using System;
using System.Collections.Generic;
using Voodoo.Messages;

namespace Fernweh.Core.Operations.Errors.Extras
{
    public class ErrorModel
    {
        public long Id { get; set; }
        public Guid GUID { get; set; }
        public bool IsProtected { get; set; }
        public Exception Exception { get; set; }
        public string ApplicationName { get; set; }
        public string MachineName { get; set; }
        public string Type { get; set; }
        public string Source { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }
        public int? ErrorHash { get; set; }
        public DateTime CreationDate { get; set; }
        public int? StatusCode { get; set; }

        public List<NameValuePair> QueryString { get; set; }
        public List<NameValuePair> Form { get; set; }
        public List<NameValuePair> Cookies { get; set; }
        public List<NameValuePair> RequestHeaders { get; set; }
        public List<NameValuePair> CustomData { get; set; }
        public int? DuplicateCount { get; set; }
        public bool IsDuplicate { get; set; }
        public string SQL { get; set; }
        public DateTime? DeletionDate { get; set; }
        public string Host { get; set; }
        public string Url { get; set; }
        public string HTTPMethod { get; set; }
        public string IPAddress { get; set; }
        public string FullJson { get; set; }
        public bool RollupPerServer { get; set; }

        public string User { get; set; }

        public int? GetHash()
        {
            return GetHashCode();
        }
    }
}