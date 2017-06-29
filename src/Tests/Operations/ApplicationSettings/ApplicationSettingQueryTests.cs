using Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Core.Operations.ApplicationSettings;
using Core.Operations.ApplicationSettings.Extras;
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
    public class ApplicationSettingQueryTests
    {
        [TestMethod]
        public async Task ApplicationSettingDetailQuery_ValidRequest_IsOk()
        {
            int? id = 0;
            using (var context = IOC.GetContext())
            {
                if (context.ApplicationSettings.Any())
                id = context.ApplicationSettings.Max(c => c.Id);
            }
            id.Should().HaveValue().Should().NotBe(0,"No data in ApplicationSetting table");
            var request = new IdRequest { Id = id.Value };
            var response = await new ApplicationSettingDetailQuery(request).ExecuteAsync();
            response.Details.Should().BeEmpty();
            response.Message.Should().BeNull();
            response.IsOk.Should().BeTrue();
            response.Data.Should().NotBeNull();
        }
        public ApplicationSettingListRequest getValidRequest()
        {
            return new ApplicationSettingListRequest();
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

