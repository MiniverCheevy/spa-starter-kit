using Fernweh.Core.Context;

namespace Fernweh.Core.Operations.Errors.Extras
{
    public class ErrorRepository
    {
        private MainContext context;

        public ErrorRepository(MainContext context)
        {
            this.context = context;
        }
    }
}