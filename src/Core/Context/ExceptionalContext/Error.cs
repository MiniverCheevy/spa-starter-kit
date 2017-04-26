using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Voodoo.Messages;

namespace Fernweh.Core.Context.ExceptionalContext
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

        public DateTime CreationDate { get; set; }

        [MaxLength(200)]
        public string Type { get; set; }

        public bool IsProtected { get; set; }

        [MaxLength(200)]
        public string Host { get; set; }

        [MaxLength(200)]
        public string Url { get; set; }

        [MaxLength(200)]
        public string HTTPMethod { get; set; }

        [MaxLength(200)]
        public string IPAddress { get; set; }

        [MaxLength(200)]
        public string Source { get; set; }

        [MaxLength(200)]
        public string Message { get; set; }

        [MaxLength]
        [Column("Detail")]
        public string Details { get; set; }

        public int? StatusCode { get; set; }

        [MaxLength]
        public string SQL { get; set; }

        public DateTime? DeletionDate { get; set; }

        [MaxLength]
        public string FullJson { get; set; }

        public int? ErrorHash { get; set; }
        public int DuplicateCount { get; set; }
        public string User { get; set; }
    }
}