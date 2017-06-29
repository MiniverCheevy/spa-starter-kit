using Core;
using Core.Models;
using Core.Context;
namespace Core.Operations.ApplicationSettings.Extras
{
    public  static partial class ApplicationSettingExtensions
    {
        public static ApplicationSettingRepository ApplicationSettingRepository(this MainContext context)
        {
            return new ApplicationSettingRepository(context);
        }
        
        public static ApplicationSettingRow ToApplicationSettingRow(this ApplicationSetting model)
        {
            var message = toApplicationSettingRow(model, new ApplicationSettingRow());
            return message;
        }
        public static ApplicationSetting UpdateFrom(this  ApplicationSetting model, ApplicationSettingRow message)
        {
            return updateFromApplicationSettingRow(message, model);
            
        }
        public static ApplicationSettingDetail ToApplicationSettingDetail(this ApplicationSetting model)
        {
            var message = toApplicationSettingDetail(model, new ApplicationSettingDetail());
            return message;
        }
        public static ApplicationSetting UpdateFrom(this  ApplicationSetting model, ApplicationSettingDetail message)
        {
            return updateFromApplicationSettingDetail(message, model);
            
        }
        
    }
}

