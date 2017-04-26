using System;
using System.Collections.Generic;
using System.Linq;
using Fernweh.Core.Models.Identity;
using Voodoo.Infrastructure.Notations;

namespace Fernweh.Core.Identity
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
        public AppPrincipal()
        {
        }



        public bool IsInRole(string role)
        {
            if (this.Roles != null)
                return this.Roles.Contains(role);
            else
                return false;
        }


        public static AppPrincipal GetAnonymousPrincipal()
        {
            return new AppPrincipal { IsAuthenticated = false, UserName = "Anonymous User" };
        }
    }
}