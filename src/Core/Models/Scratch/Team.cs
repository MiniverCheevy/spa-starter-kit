using System.Collections.Generic;

namespace Core.Models.Scratch
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Member> Members { get; set; } = new List<Member>();
        public List<Project> Projects { get; set; } = new List<Project>();
    }
}