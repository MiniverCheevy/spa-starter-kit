using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models.Exceptions
{
    [Table("Exceptions")]
    public class Error
    {
        public long Id { get; set; }

        [Index]
        public Guid GUID { get; set; }

        [MaxLength(200)]
        public string ApplicationName { get; set; }

        [MaxLength(200)]
        public string MachineName { get; set; }

        public DateTimeOffset CreationDate { get; set; }

        [MaxLength(200)]
        public string Type { get; set; }

        public bool IsProtected { get; set; }

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
        public string Sql { get; set; }

        public DateTimeOffset? DeletionDate { get; set; }

        [MaxLength]
        public string FullJson { get; set; }

        public int? ErrorHash { get; set; }
        public int DuplicateCount { get; set; }
        public string User { get; set; }
    }
}