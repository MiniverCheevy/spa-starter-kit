using Fernweh.Core.Context;

namespace Fernweh.Core.Operations.Errors.Extras
{
    public class ErrorRepository
    {
        private FernwehContext context;

        public ErrorRepository(FernwehContext context)
        {
            this.context = context;
        }
    }
}