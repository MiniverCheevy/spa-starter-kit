using Core.Models.Identity;
using Core.Operations.Users.Extras;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Voodoo.TestData;

namespace Fernweh.Tests.Operations.Users
{
    [TestClass]
    public class UserMappingTests
    {
        private Randomizer randomizer = new Randomizer();

        private User arrange()
        {
            TestHelper.SetRandomDataSeed(1);
            var source = new User();
            TestHelper.Randomizer.Randomize(source);
            return source;
        }

        [TestMethod]
        public void Map_Message_PropertiesTheSame()
        {
            var testHelper =
                new MappingTesterHelper<User, UserMessage>();
            var source = arrange();
            var message = source.ToUserMessage();
            var target = new User();
            target.UpdateFrom(message);

            testHelper.Compare(source, message, new string[] { });
            testHelper.Compare(target, message, new[] {"Id"});
        }

        [TestMethod]
        public void Map_Detail_PropertiesTheSame()
        {
            var testHelper =
                new MappingTesterHelper<User, UserDetail>();
            var source = arrange();
            var message = source.ToUserDetail();
            var target = new User();
            target.UpdateFrom(message);

            testHelper.Compare(source, message, new string[] { });
            testHelper.Compare(target, message, new[] {"Id"});
        }
    }
}