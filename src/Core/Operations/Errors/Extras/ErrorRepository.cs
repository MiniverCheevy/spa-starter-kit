using Fernweh.Core.Context;

namespace Fernweh.Core.Operations.Errors.Extras
{
    public class ErrorRepository
    {
        private AppContext context;

        public ErrorRepository(AppContext context)
        {
            this.context = context;
        }
    }
}