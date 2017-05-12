using Fernweh.Core.Context;

namespace Fernweh.Core.Operations.ApplicationSettings.Extras
{
    public class ApplicationSettingRepository
    {
        private AppContext context;

        public ApplicationSettingRepository(AppContext context)
        {
            this.context = context;
        }
    }
}