using System.Threading.Tasks;
using Fernweh.Core.Operations.ApplicationSettings;
using Fernweh.Core.Operations.ApplicationSettings.Extras;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fernweh.Tests.Operations.ApplicationSettings
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

