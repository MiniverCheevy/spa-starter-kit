using System.Threading.Tasks;
using Core.Operations.ApplicationSettings;
using Core.Operations.ApplicationSettings.Extras;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Operations.ApplicationSettings
{
    [TestClass]
    public class ApplicationSettingAddCommandTests
    {
        [TestMethod]
        public async Task ApplicationSettingAddCommand_ValidRequest_IsOk()
        {
            var request = ApplicationSettingTestHelper.GetNewApplicationSetting();
            var command = new ApplicationSettingSaveCommand(request);

            var response = await command.ExecuteAsync();

            response.Details.Should().BeEmpty();
            response.Message.Should().Be(ApplicationSettingMessages.AddOk);
            response.IsOk.Should().BeTrue();
            response.NewItemId.Should().NotBe(0);
        }
    }
}