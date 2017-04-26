﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fernweh.Core.Security;
using Voodoo.Messages;
using Voodoo.Operations.Async;
using Fernweh.Core.Operations.Users.Extras;
using Voodoo;
using System.Data.Entity;
using System.Security;
using Fernweh.Core.Context;
using Fernweh.Core.Identity;
using Fernweh.Core.Models.Identity;
using Fernweh.Core.Operations.CurrentUsers.Extras;

namespace Fernweh.Core.Operations.CurrentUsers
{
    public class BuildPrincipalCommand : CommandAsync<BuildPrincipalRequest, Response<AppPrincipal>>
    {
        private FernwehContext context;
        private User user;

        public BuildPrincipalCommand(BuildPrincipalRequest request) : base(request)
        {
        }

        protected override async Task<Response<AppPrincipal>> ProcessRequestAsync()
        {
            using (context = IOC.GetContext())
            {
                user = await context.UserRepository().GetUserAndRolesQuery()
                    .Where(c => c.UserName == request.UserName)
                    .FirstOrDefaultAsync();

                user.ThrowIfNull(UserMessages.NotFound);

                if (user.LockoutEnabled)
                    throw new SecurityException("Your account is locked.");

                response.Data = buildPrincipal();

                await updateDatabase();
            }
            return response;
        }

        private AppPrincipal buildPrincipal()
        {
            var principal = user.ToAppPrincipal();
            principal.IsAuthenticated = true;            
            principal.Expiration = DateTime.UtcNow.AddDays(1);
            principal.RefreshTime = DateTime.UtcNow.AddMinutes(5);
            principal.Token = Encryption.Encrypt(principal);
            return principal;
        }
        private async Task updateDatabase()
        {
            user.LastAuthentication = DateTime.UtcNow;
            user.LastUserAgent = request.UserAgent.Truncate(256);
            await context.SaveChangesAsync();
        }
    }
}
