using System;
using System.Collections.Generic;

namespace Core.Models.Scratch
{
    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public List<Team> Teams { get; set; } = new List<Team>();
        public int RequiredInt { get; set; }
        public int? OptionalInt { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime? OptionalDate { get; set; }
        public decimal RequiredDecimal { get; set; }
        public decimal? OptionalDecimal { get; set; }
        public int? ManagerId { get; set; }
        public Member Manager { get; set; }
    }
}