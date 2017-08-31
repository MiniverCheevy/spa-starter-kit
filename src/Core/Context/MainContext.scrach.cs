using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models.Scratch;

namespace Core.Context
{
    public partial class DatabaseContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Team> Teams { get; set; }
    }
}
