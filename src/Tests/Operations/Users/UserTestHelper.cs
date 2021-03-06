
using Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Core.Operations.Users;
using Core.Operations.Users.Extras;
using Voodoo.TestData;
using Core.Context;
using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;
using Core.Models.Identity;
using Core.Operations.Lists;
using Core.Operations.Roles.Extras;
using Voodoo;
using Voodoo.Messages;
namespace Tests.Operations.Users
{
    [TestClass]
    public class UserTestHelper
    {
        public static UserDetail GetNewUser()
        {
            var request= new UserDetail();
            TestHelper.Randomizer.Randomize(request);
            const string password = "password";
            request.Password = password;
            request.ConfirmPassword = password;
            request.UserName = TestHelper.Data.Person().EmailAddress;
            request.Roles.Add(new ListItem() { Id = 1 });
            return request;
        }
        
        public static async Task<UserDetail> GetExistingUser()
        {
            var request= GetNewUser();
            var command = new UserSaveCommand(request);
            var response = await command.ExecuteAsync();
            
            response.Details.Should().BeEmpty();
            response.Message.Should().Be(UserMessages.AddOk);
            response.IsOk.Should().BeTrue();
            response.NewItemId.Should().NotBe(0);
            
            var query = new UserDetailQuery(new IdRequest{Id =response.NewItemId});
            var data= await query.ExecuteAsync();
            return data.Data;
        }
    }
}

