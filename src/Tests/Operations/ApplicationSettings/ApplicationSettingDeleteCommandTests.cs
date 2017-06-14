using System.Threading.Tasks;
using Core.Operations.ApplicationSettings;
using Core.Operations.ApplicationSettings.Extras;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Voodoo.Messages;

namespace Fernweh.Tests.Operations.ApplicationSettings
{
    [TestClass]
    public class ApplicationSettingDeleteCommandTests
    {
        [TestMethod]
        public async Task ApplicationSettingDeleteCommand_ValidRequest_IsOk()
        {
            var model = await ApplicationSettingTestHelper.GetExistingApplicationSetting();
            var request = new IdRequest {Id = model.Id};
            var command = new ApplicationSettingDeleteCommand(request);

            var response = await command.ExecuteAsync();

            response.Details.Should().BeEmpty();
            response.Message.Should().Be(ApplicationSettingMessages.DeleteOk);
            response.IsOk.Should().BeTrue();
        }
    }
}