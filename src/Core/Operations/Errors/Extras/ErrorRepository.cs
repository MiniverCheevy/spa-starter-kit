using Core.Context;

namespace Core.Operations.Errors.Extras
{
    public class ErrorRepository
    {
        private DatabaseContext context;

        public ErrorRepository(DatabaseContext context)
        {
            this.context = context;
        }
    }
}