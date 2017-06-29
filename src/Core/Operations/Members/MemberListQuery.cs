using Core;
using Core.Models.Scratch;
using Core.Operations.Members.Extras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Voodoo;
using Voodoo.Infrastructure;
using Voodoo.Messages;
using Voodoo.Operations;
using Voodoo.Operations.Async;
using Voodoo.Validation.Infrastructure;
using Core.Context;
using System.Data.Entity;
namespace Core.Operations.Members
{
    [Rest(Verb.Get, RestResources.MemberList)]
    public class MemberListQuery : QueryAsync<MemberListRequest, MemberListResponse>
    {
        private MainContext context;
        private IValidator validator = ValidationManager.GetDefaultValidatitor();
        public MemberListQuery(MemberListRequest request) : base(request)
        {
        }
        protected override async Task<MemberListResponse> ProcessRequestAsync()
        {
            using (context = IOC.GetContext())
            {
                var query = context.Members.AsNoTracking().AsQueryable();
                var data = await query.ToPagedResponseAsync(request, c => c.ToMemberRow());
                response.From(data, c => c);
            }
            return response;
        }
    }
}
