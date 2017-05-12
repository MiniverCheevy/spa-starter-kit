using Fernweh.Core.Context;
using Fernweh.Core.Models;

namespace Fernweh.Core.Operations.ApplicationSettings.Extras
{
    public static partial class ApplicationSettingExtensions
    {
        public static ApplicationSettingRepository ApplicationSettingRepository(this AppContext context)
        {
            return new ApplicationSettingRepository(context);
        }

        public static ApplicationSettingMessage ToApplicationSettingMessage(this ApplicationSetting model)
        {
            var message = toApplicationSettingMessage(model, new ApplicationSettingMessage());
            return message;
        }

        public static ApplicationSetting UpdateFrom(this ApplicationSetting model, ApplicationSettingMessage message)
        {
            return updateFromApplicationSettingMessage(message, model);
        }
    }
}