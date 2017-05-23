using Core.Context;

namespace Core.Operations.Errors.Extras
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