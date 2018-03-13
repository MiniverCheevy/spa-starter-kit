using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Core.Models.Scratch
{
    [Owned]
    [Table("Teams", Schema = "scratch")]
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }

        
    }
}