using System.Linq;
using System.Threading.Tasks;
using Core.Context;
using Core.Models.Mappings;
using Core.Operations.Errors.Extras;
using Microsoft.EntityFrameworkCore;
using Voodoo;
using Voodoo.Infrastructure;
using Voodoo.Operations.Async;

namespace Core.Operations.Errors
{
    [Rest(Verb.Get, RestResources.ErrorLogList)]
    public class ErrorListQuery : QueryAsync<ErrorListRequest, ErrorListResponse>
    {
        private DatabaseContext context;

        public ErrorListQuery(ErrorListRequest request) : base(request)
        {
        }

        protected override async Task<ErrorListResponse> ProcessRequestAsync()
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

                var data = await query.ToPagedResponseAsync(request, c => c.ToErrorRow());
                response.From(data, c => c);
            }

            return response;
        }
    }
}