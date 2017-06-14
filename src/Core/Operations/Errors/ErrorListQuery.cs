using System.Linq;
using System.Threading.Tasks;
using Core.Context;
using Core.Operations.Errors.Extras;
using Voodoo;
using Voodoo.Infrastructure;
using Voodoo.Operations.Async;

namespace Core.Operations.Errors
{
    [Rest(Verb.Get, RestResources.ErrorList)]
    public class ErrorListQuery : QueryAsync<ErrorQueryRequest, ErrorQueryResponse>
    {
        private MainContext context;

        public ErrorListQuery(ErrorQueryRequest request) : base(request)
        {
        }

        protected override async Task<ErrorQueryResponse> ProcessRequestAsync()
        {
            using (context = IOC.GetContext())
            {
                var query = context.Errors.AsNoTracking().AsQueryable();
                if (!string.IsNullOrWhiteSpace(request.SearchText))
                    query = query.Where(c => c.Details.Contains(request.SearchText) ||
                                             c.Type.Contains(request.SearchText) ||
                                             c.Url.Contains(request.SearchText) ||
                                             c.Message.Contains(request.SearchText) ||
                                             c.User.Contains(request.SearchText)
                    );

                var data = await query.ToPagedResponseAsync(request, c => c.ToErrorMessage());
                response.From(data, c => c);
            }

            return response;
        }
    }
}