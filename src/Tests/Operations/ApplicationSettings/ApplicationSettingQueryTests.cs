using System.Linq;
using System.Threading.Tasks;
using Core;
using Core.Operations.ApplicationSettings;
using Core.Operations.ApplicationSettings.Extras;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Voodoo.Messages;

namespace Fernweh.Tests.Operations.ApplicationSettings
{
    [TestClass]
    public class ApplicationSettingQueryTests
    {
        [TestMethod]
        public async Task ApplicationSettingDetailQuery_ValidRequest_IsOk()
        {
            int? id = null;
            using (var context = IOC.GetContext())
            {
                id = context.ApplicationSettings.Max(c => c.Id);
            }
            id.Should().HaveValue().Should().NotBe(0,
                "No data in ApplicationSetting table");
            var request = new IdRequest {Id = id.Value};
            var response = await new ApplicationSettingDetailQuery(request).ExecuteAsync();
            response.Details.Should().BeEmpty();
            response.Message.Should().BeNull();
            response.IsOk.Should().BeTrue();
            response.Data.Should().NotBeNull();
        }

        public ApplicationSettingQueryRequest getValidRequest()
        {
            return new ApplicationSettingQueryRequest();
        }

        [TestMethod]
        public async Task ApplicationSettingListQuery_ValidRequest_IsOk()
        {
            var request = getValidRequest();
            var response = await new ApplicationSettingListQuery(request).ExecuteAsync();
            response.Details.Should().BeEmpty();
            response.Message.Should().BeNull();
            response.IsOk.Should().BeTrue();
            response.Data.Should().NotBeNull();
            response.Data.Should().NotBeEmpty();
        }
    }
}