using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models.Scratch;
using Voodoo.CodeGeneration.Models;

namespace Core.Context
{
    public partial class DatabaseContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Team> Teams { get; set; }

        public DbSet<TestClass> TestClasses { get; set; }
    }
}
