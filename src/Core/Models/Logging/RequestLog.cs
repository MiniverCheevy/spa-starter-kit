using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Voodoo;

namespace Core.Models.Logging
{
    public class RequestLog
    {
        public long Id { get; set; }

        [MaxLength(200)]
        public string MachineName { get; set; }

        //[Index]
        public DateTimeOffset CreationDate { get; set; }

        [MaxLength(200)]
        public string Host { get; set; }

        [MaxLength(200)]
        public string Url { get; set; }

        [MaxLength(200)]
        public string HttpMethod { get; set; }

        [MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(200)]
        public decimal DurationInMs { get; set; }
                
        public string User { get; set; }
        [MaxLength(8000)]
        public string UserAgent { get; set; }

        [MaxLength(200)]
        public string BrowserFamily { get; set; }
        [MaxLength(200)]
        public string BrowserVersion { get; set; }
        [MaxLength(200)]
        public string OSFamily { get; set; }
        //[Index]
        public Guid? RequestId { get; set; }

        [MaxLength]
        public string TraceLogs { get; set; }
        public int WeekOfYear { get; set; }
        public int DayOfYear { get; set; }
    }
}