
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
    public class ApplicationSettingUpdateCommandTests
    {
        [TestMethod]
        public async Task ApplicationSettingUpdateCommand_ValidRequest_IsOk()
        {
            var request = await ApplicationSettingTestHelper.GetExistingApplicationSetting();
            var command = new ApplicationSettingSaveCommand(request);
            var response = await command.ExecuteAsync();
            response.Details.Should().BeEmpty();
            response.Message.Should().Be(ApplicationSettingMessages.UpdateOk);
            response.IsOk.Should().BeTrue();
        }
        
    }
}

