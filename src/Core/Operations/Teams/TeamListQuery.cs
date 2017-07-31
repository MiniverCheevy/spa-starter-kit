using Core;
using Core.Models.Scratch;
using Core.Operations.Teams.Extras;
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
namespace Core.Operations.Teams
{
    [Rest(Verb.Get, RestResources.TeamList)]
    public class TeamListQuery:QueryAsync<TeamListRequest,TeamListResponse>
    {
        private MainContext context;
        private IValidator validator = ValidationManager.GetDefaultValidatitor();
        public TeamListQuery (TeamListRequest request) : base(request)
        {
        }
        protected override async Task<TeamListResponse> ProcessRequestAsync()
        {
            using (context = IOC.GetContext())
            {
                var query = context.Teams.AsNoTracking().AsQueryable();
                var data = await query.ToPagedResponseAsync(request, c => c.ToTeamRow());
                response.From(data, c=>c);
            }
            return response;
        }
    }
}

