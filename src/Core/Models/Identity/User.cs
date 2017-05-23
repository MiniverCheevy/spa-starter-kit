using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Voodoo.Infrastructure.Notations;

namespace Core.Models.Identity
{
    public class User
    {
        public int Id { get; set; }

        [Index(IsUnique = true)]
        public string UserName { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Secret]
        public string Salt { get; set; }

        [Secret]
        public string PasswordHash { get; set; }

        public bool LockoutEnabled { get; set; }

        public string LastUserAgent { get; set; }
        public DateTime? LastAuthentication { get; set; }

        public List<Role> Roles { get; set; }

        public User()
        {
            Roles = new List<Role>();
        }
    }

    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<User> Users { get; set; }

        public Role()
        {
            Users = new List<User>();
        }
    }
}