using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models.Scratch
{
    [Table("Members", Schema = "scratch")]
    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }        
        public int RequiredInt { get; set; }
        public int? OptionalInt { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime? OptionalDate { get; set; }

        public DateTimeOffset RequiredDateTimeOffset { get; set; }
        public DateTimeOffset? OptionalDateTimeOffset { get; set; }

        public decimal RequiredDecimal { get; set; }
        public decimal? OptionalDecimal { get; set; }
        public int? ManagerId { get; set; }
        public Member Manager { get; set; }
        public List<Team> Teams { get; set; } = new List<Team>();
        public List<BlobOfText> BlobsOfText { get; set; } = new List<BlobOfText>();
    }
}