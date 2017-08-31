
using Core;
using Core.Models.Scratch;
using Core.Operations.Teams.Extras;
using Core.Models.Mappings;
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
    [Rest(Verb.Get, RestResources.Team)]
    public class TeamDetailQuery : QueryAsync<IdRequest,Response<TeamDetail>>
    {
        private DatabaseContext context;
        private IValidator validator = ValidationManager.GetDefaultValidatitor();
        
        public TeamDetailQuery (IdRequest request) : base(request)
        {
        }
        
        protected override async Task<Response<TeamDetail>> ProcessRequestAsync()
        {
            var model = new Team();
            if (request.Id != 0)
            {
                using(context = IOC.GetContext())
                {
                    var query = context.Teams.AsNoTracking().AsQueryable()
                                       .Where(c=>c.Id == request.Id);
                    
                    model = await query.FirstOrDefaultAsync();
                    if (model == null)
                    throw new Exception(TeamMessages.NotFound);
                }
                
            }
            response.Data = model.ToTeamDetail();
            return response;
        }
    }
}

