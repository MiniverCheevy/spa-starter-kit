using System;
using System.Collections.Generic;
using Fernweh.Core;
using Fernweh.Core.Identity;
using Fernweh.Core.Infrastructure;

namespace Fernweh.Tests.Fakes
{
    internal class FakeRequestContextProvider : IRequestContextProvider
    {
        public RequestContext RequestContext => new RequestContext
        {
            AppPrincipal = new AppPrincipal
            {
                Expiration = DateTime.MaxValue,
                IsAuthenticated = true,
                FirstName = "Test",
                LastName = "Testerson",
                RefreshTime = DateTime.MaxValue,
                Roles = new List<string> {RoleNames.Administrator},
                UserId = 1,
                UserName = "test.testerson@mailenator.com"
            },
            ClientInfo = new ClientInfo()
        };
    }
}