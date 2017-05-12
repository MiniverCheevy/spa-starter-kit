using Fernweh.Core.Context;

namespace Fernweh.Core.Operations.ApplicationSettings.Extras
{
    public class ApplicationSettingRepository
    {
        private MainContext context;

        public ApplicationSettingRepository(MainContext context)
        {
            this.context = context;
        }
    }
}