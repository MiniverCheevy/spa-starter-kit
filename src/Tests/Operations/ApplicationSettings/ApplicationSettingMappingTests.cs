using Core.Models;
using Core.Operations.ApplicationSettings.Extras;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Voodoo.TestData;

namespace Tests.Operations.ApplicationSettings
{
    [TestClass]
    public class ApplicationSettingMappingTests
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
        public void Map_Message_PropertiesTheSame()
        {
            var testHelper =
                new MappingTesterHelper<ApplicationSetting, ApplicationSettingMessage>();
            var source = arrange();
            var message = source.ToApplicationSettingMessage();
            var target = new ApplicationSetting();
            target.UpdateFrom(message);

            testHelper.Compare(source, message, new string[] {});
            testHelper.Compare(target, message, new[] {"Id"});
        }
    }
}