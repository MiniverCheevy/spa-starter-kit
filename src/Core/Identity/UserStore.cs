//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Security;
//using System.Security.Claims;
//using System.Threading;
//using System.Threading.Tasks;
//using Fernweh.Core.Models.Identity;
//using Fernweh.Core.Security;
//using Voodoo;
//using Voodoo.Infrastructure;
//using Voodoo.Logging;
//using Voodoo.Messages;

//namespace Fernweh.Core.Identity
//{
//    public class UserStore
//    {
//        public const string LoginFailure = "Invalid Username or password";
//        public const string UserNotFound = "User not found";
//        public const string InvalidPassword = "Invalid password";
//        private readonly IIdentityContext context;
//        private readonly PasswordManager manager = new PasswordManager();

//        public UserStore(IIdentityContext context)
//        {
//            this.context = context;
//        }

//        private IQueryable<User> UserQuery
//        {
//            get { return context.Users.Include(c => c.Roles); }
//        }

//        ~UserStore()
//        {
//            context.Dispose();
//        }

//        public async Task<Response<User>> FindByIdAsync(int userId)
//        {
//            return await ActionHandler.ExecuteAsync(async () =>
//            {
//                var response = new Response<User>
//                    {Data = await UserQuery.FirstOrDefaultAsync(c => c.Id == userId)};
//                return response;
//            });
//        }

//        public Task<Response<ClaimsIdentity>> CreateIdentityAsync(User user)
//        {
//            return Task.FromResult(new ClaimsIdentityFactory(user).CreateIdentity());
//        }

//        public static AppPrincipal GetPrincipal(IEnumerable<Claim> userClaims)
//        {
//            return new PrincipalFactory(userClaims).CreateUserFromClaims();
//        }

//        public static AppPrincipal GetAnonymousPrinciple()
//        {
//            var identity = new AppIdentity {IsAuthenticated = false};
//            var principal = new AppPrincipal(identity) {};
//            return principal;
//        }

//        public async Task<Response<User>> FindAsync(string userName, string password)
//        {
//            return await ActionHandler.ExecuteAsync(async () =>
//            {
//                var user = await UserQuery.FirstOrDefaultAsync(c => c.UserName == userName);
//                if (user == null)
//                    throw new SecurityException(LoginFailure);

//                if (!manager.Compare(password, user.Salt, user.PasswordHash))
//                    throw new SecurityException(LoginFailure);

//                var response = new Response<User> {Data = user};
//                return response;
//            });
//        }


//        public async Task<Response> ChangePasswordAsync(int userId, string oldPassword, string newPassword)
//        {
//            return await ActionHandler.ExecuteAsync(async () =>
//            {
//                var response = new Response();
//                var user = await UserQuery.FirstOrDefaultAsync(c => c.Id == userId);
//                if (user == null)
//                    throw new InvalidOperationException(UserNotFound);

//                if (!manager.Compare(oldPassword, user.Salt, user.PasswordHash))
//                    throw new ArgumentException(InvalidPassword);

//                var password = manager.CreateHash(newPassword, user.Salt);
//                user.PasswordHash = password;
//                await context.SaveChangesAsync();

//                return response;
//            });
//        }

//        public async Task<Response> AddPasswordAsync(int userId, string newPassword)
//        {
//            var response = new Response();
//            var user = await UserQuery.FirstOrDefaultAsync(c => c.Id == userId);
//            if (user == null)
//                throw new InvalidOperationException(UserNotFound);

//            var password = manager.CreateHash(newPassword, user.Salt);
//            user.PasswordHash = password;
//            await context.SaveChangesAsync();

//            return response;
//        }
//    }
//}

