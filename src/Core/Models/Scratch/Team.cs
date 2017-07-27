using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models.Scratch
{
    [Table("Teams", Schema = "scratch")]
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Member> Members { get; set; } = new List<Member>();
        public List<Project> Projects { get; set; } = new List<Project>();
    }
}