using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fernweh.Core.Infrastructure;
using Voodoo;
using Voodoo.Infrastructure;
using Voodoo.Operations.Async;

namespace Fernweh.Core.Operations.Lists
{
    [Rest(Verb.Get, RestResources.Lists)]
    public class LookupsQuery : QueryAsync<ListsRequest, ListsResponse>
    {
        private ListsHelper helper = new ListsHelper();

        public LookupsQuery(ListsRequest request) : base(request)
        {
        }

        protected override async Task<ListsResponse> ProcessRequestAsync()
        {
            var listRequest = new ListsRequest
            {
                IncludeInactive = request.IncludeInactive,
                Lists = request.Lists.Select(c => c.To<Lists>()).ToList()
            };
            using (var context = IOC.GetContext())
            {
                response = await Task.Run(() => helper.GetLists(context, listRequest));
            }
            return response;
        }
    }
}