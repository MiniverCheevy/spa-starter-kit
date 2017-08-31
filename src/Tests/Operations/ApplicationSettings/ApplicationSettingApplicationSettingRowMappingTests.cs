using Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Core.Operations.ApplicationSettings;
using Core.Operations.ApplicationSettings.Extras;
using Core.Models.Mappings;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;
using Voodoo;
using Voodoo.Messages;
using Voodoo.TestData;
namespace Tests.Operations.ApplicationSettings
{
    [TestClass]
    public class ApplicationSettingApplicationSettingRowMappingTests
    {
        private Randomizer randomizer = new Randomizer();
        
        private ApplicationSetting arrange()
        {
            TestHelper.SetRandomDataSeed(1);
            var source = new ApplicationSetting();
            TestHelper.Randomizer.Randomize(source);
            return source;
        }
        
        [TestMethod]
        public void Map_MapBack_PropertiesTheSame()
        {
            var testHelper =
            new MappingTesterHelper<ApplicationSetting, ApplicationSettingRow>();
            var source = arrange();
            var message = source.ToApplicationSettingRow();
            var target = new ApplicationSetting();
            target.UpdateFrom(message);
            testHelper.Compare(source, message, new string[]{});
            testHelper.Compare(target, message, new[] { "Id" });
        }
    }
}

