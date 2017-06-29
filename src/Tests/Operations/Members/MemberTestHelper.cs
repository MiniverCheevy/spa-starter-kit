
using Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Core.Operations.Members;
using Core.Operations.Members.Extras;
using Voodoo.TestData;
using Core.Context;
using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;
using Voodoo;
using Voodoo.Messages;
namespace Tests.Operations.Members
{
    [TestClass]
    public class MemberTestHelper
    {
        public static MemberDetail GetNewMember()
        {
            var request= new MemberDetail();
            TestHelper.Randomizer.Randomize(request);
            return request;
        }
        
        public static async Task<MemberDetail> GetExistingMember()
        {
            var request= GetNewMember();
            var command = new MemberSaveCommand(request);
            var response = await command.ExecuteAsync();
            
            response.Details.Should().BeEmpty();
            response.Message.Should().Be(MemberMessages.AddOk);
            response.IsOk.Should().BeTrue();
            response.NewItemId.Should().NotBe(0);
            
            var query = new MemberDetailQuery(new IdRequest{Id =response.NewItemId});
            var data= await query.ExecuteAsync();
            return data.Data;
        }
    }
}

