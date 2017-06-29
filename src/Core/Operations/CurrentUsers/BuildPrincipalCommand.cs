using System;
using System.Data.Entity;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using Core.Context;
using Core.Identity;
using Core.Models.Identity;
using Core.Operations.CurrentUsers.Extras;
using Core.Operations.Users.Extras;
using Core.Security;
using Voodoo;
using Voodoo.Messages;
using Voodoo.Operations.Async;

namespace Core.Operations.CurrentUsers
{
    public class BuildPrincipalCommand : CommandAsync<BuildPrincipalRequest, Response<AppPrincipal>>
    {
        private MainContext context;
        private User user;

        public BuildPrincipalCommand(BuildPrincipalRequest request) : base(request)
        {
        }

        protected override async Task<Response<AppPrincipal>> ProcessRequestAsync()
        {
            using (context = IOC.GetContext())
            {
#if DEBUG
                if (request.UserName == "shawn")
                    request.UserName = "shawn.doucet";
#endif
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