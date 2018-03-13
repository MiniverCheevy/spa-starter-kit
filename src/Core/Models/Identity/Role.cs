using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Core.Models.Identity
{
    [Owned]
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public Role()
        {
        
        }
    }
}