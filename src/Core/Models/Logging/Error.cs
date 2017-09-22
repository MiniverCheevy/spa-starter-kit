using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Voodoo;

namespace Core.Models.Logging
{
    [Table("Exceptions")]
    public class Error
    {
        public long Id { get; set; }

        [MaxLength(200)]
        public string MachineName { get; set; }

        [Index]
        public DateTimeOffset CreationDate { get; set; }

        [Index]
        [MaxLength(200)]
        public string Type { get; set; }

        [MaxLength(200)]
        public string Host { get; set; }

        [MaxLength(200)]
        public string Url { get; set; }

        [MaxLength(200)]
        public string HttpMethod { get; set; }

        [MaxLength(200)]
        public string IpAddress { get; set; }

        [MaxLength(200)]
        public string Source { get; set; }

        [MaxLength(200)]
        public string Message { get; set; }

        [MaxLength]
        [Column("Detail")]
        public string Details { get; set; }

        public int? StatusCode { get; set; }

        [MaxLength]
        public string FullJson { get; set; }

        public int? ErrorHash { get; set; }
        public string User { get; set; }

        [Index]
        public Guid? RequestId { get; set; }
    }
}