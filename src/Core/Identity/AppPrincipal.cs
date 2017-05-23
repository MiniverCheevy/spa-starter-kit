using System;
using System.Collections.Generic;
using System.Linq;
using Core.Models.Identity;
using Voodoo.Infrastructure.Notations;

namespace Core.Identity
{
    [Client]
    [MapsTo(typeof(User))]
    public class AppPrincipal
    {
        public DateTime Expiration { get; set; }
        public DateTime RefreshTime { get; set; }
        public bool IsAuthenticated { get; set; }
        public string UserName { get; set; }
        public int? UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<string> Roles { get; set; } = new List<string>();
        public bool IsAdmin => IsInRole(RoleNames.Administrator);
        public string Token { get; set; }


        public bool IsInRole(string role)
        {
            if (Roles != null)
                return Roles.Contains(role);
            return false;
        }


        public static AppPrincipal GetAnonymousPrincipal()
        {
            return new AppPrincipal {IsAuthenticated = false, UserName = "Anonymous User"};
        }
    }
}