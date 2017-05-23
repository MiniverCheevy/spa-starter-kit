using System.Threading.Tasks;
using Core.Operations.ApplicationSettings;
using Core.Operations.ApplicationSettings.Extras;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Voodoo.Messages;
using Voodoo.TestData;

namespace Tests.Operations.ApplicationSettings
{
    [TestClass]
    public class ApplicationSettingTestHelper
    {
        public static ApplicationSettingMessage GetNewApplicationSetting()
        {
            var request = new ApplicationSettingMessage();
            TestHelper.Randomizer.Randomize(request);
            return request;
        }

        public static async Task<ApplicationSettingMessage> GetExistingApplicationSetting()
        {
            var request = GetNewApplicationSetting();
            var command = new ApplicationSettingSaveCommand(request);
            var response = await command.ExecuteAsync();

            response.Details.Should().BeEmpty();
            response.Message.Should().Be(ApplicationSettingMessages.AddOk);
            response.IsOk.Should().BeTrue();
            response.NewItemId.Should().NotBe(0);

            var query = new ApplicationSettingDetailQuery(new IdRequest {Id = response.NewItemId});
            var data = await query.ExecuteAsync();
            return data.Data;
        }
    }
}