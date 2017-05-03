using Microsoft.Extensions.Configuration;

namespace React.Infrastructure.Settings
{
    public static class SettingsFactory
    {
        public static Fernweh.Core.Infrastructure.Settings GetSettings(IConfigurationRoot configuration)
        {
            return new Fernweh.Core.Infrastructure.Settings
            {
                DefaultConnectionString = configuration.GetConnectionString("DefaultConnection")
            };
        }
    }
}