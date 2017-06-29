
using Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Core.Operations.ApplicationSettings;
using Core.Operations.ApplicationSettings.Extras;
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
namespace Tests.Operations.ApplicationSettings
{
    [TestClass]
    public class ApplicationSettingTestHelper
    {
        public static ApplicationSettingDetail GetNewApplicationSetting()
        {
            var request= new ApplicationSettingDetail();
            TestHelper.Randomizer.Randomize(request);
            return request;
        }
        
        public static async Task<ApplicationSettingDetail> GetExistingApplicationSetting()
        {
            var request= GetNewApplicationSetting();
            var command = new ApplicationSettingSaveCommand(request);
            var response = await command.ExecuteAsync();
            
            response.Details.Should().BeEmpty();
            response.Message.Should().Be(ApplicationSettingMessages.AddOk);
            response.IsOk.Should().BeTrue();
            response.NewItemId.Should().NotBe(0);
            
            var query = new ApplicationSettingDetailQuery(new IdRequest{Id =response.NewItemId});
            var data= await query.ExecuteAsync();
            return data.Data;
        }
    }
}

