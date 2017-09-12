using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Core.Context;
using Core.Operations.Errors.Extras;
using Voodoo.Infrastructure;
using Voodoo.Messages;
using Voodoo.Operations.Async;

namespace Core.Operations.Errors
{
    [Rest(Verb.Get, RestResources.ErrorLog)]
    public class ErrorDetailQuery : QueryAsync<IdRequest, Response<ErrorDetail>>
    {
        private DatabaseContext context;

        public ErrorDetailQuery(IdRequest request) : base(request)
        {
        }

        protected override async Task<Response<ErrorDetail>> ProcessRequestAsync()
        {
            using (context = IOC.GetContext())
            {
                var error =
                    await
                        context.Errors.AsNoTracking()
                            .AsQueryable()
                            .Where(c => c.Id == request.Id)
                            .Select(c => c.FullJson)
                            .FirstOrDefaultAsync();
                if (error != null)
                {
                    response.Data = ErrorJsonDeserializer.Deserialize(error);
                }
                else
                {
                    response.IsOk = false;
                    response.Message = "Error not found";
                }
            }
            return response;
        }
    }
}