
using Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Core.Operations.ApplicationSettings;
using Core.Operations.ApplicationSettings.Extras;
using Voodoo.TestData;
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
    public class ApplicationSettingDeleteCommandTests
    {
        [TestMethod]
        public async Task ApplicationSettingDeleteCommand_ValidRequest_IsOk()
        {
            var model = await ApplicationSettingTestHelper.GetExistingApplicationSetting();
            var request = new IdRequest { Id=model.Id };
            var command = new ApplicationSettingDeleteCommand(request);
            
            var response = await command.ExecuteAsync();
            
            response.Details.Should().BeEmpty();
            response.Message.Should().Be(ApplicationSettingMessages.DeleteOk);
            response.IsOk.Should().BeTrue();
        }
        
    }
}

