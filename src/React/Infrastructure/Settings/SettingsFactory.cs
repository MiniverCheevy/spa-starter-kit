using Microsoft.Extensions.Configuration;

namespace Fernweh.Aurelia.Infrastructure.Settings
{
    public static class SettingsFactory
    {
        public static Core.Infrastructure.Settings GetSettings(IConfigurationRoot configuration)
        {
            return new Core.Infrastructure.Settings
            {
                DefaultConnectionString = configuration.GetConnectionString("DefaultConnection")
            };
        }
    }
}