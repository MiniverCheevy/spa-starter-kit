using Fernweh.Core.Context;

namespace Fernweh.Core.Operations.ApplicationSettings.Extras
{
    public class ApplicationSettingRepository
    {
        private FernwehContext context;

        public ApplicationSettingRepository(FernwehContext context)
        {
            this.context = context;
        }
    }
}