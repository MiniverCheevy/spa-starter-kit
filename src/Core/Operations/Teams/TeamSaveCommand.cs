
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
    [Rest(Verb.Put, RestResources.Team)]
    public class TeamSaveCommand :CommandAsync<TeamDetail, NewItemResponse>
    {
        private bool isNew;
        private MainContext context;
        private IValidator validator = ValidationManager.GetDefaultValidatitor();
        public TeamSaveCommand(TeamDetail request) : base(request)
        {
        }
        
        protected override async Task<NewItemResponse> ProcessRequestAsync()
        {
            //The request object is validated by default, validate anything else with
            //validator.Validate(<something>);
            
            using(context = IOC.GetContext())
            {
                var model = await createOrGetExisting();
                model.ThrowIfNull(TeamMessages.NotFound);
                
                model.UpdateFrom(request);
                await context.SaveChangesAsync();
                
                response.NewItemId = model.Id;
            }
            response.Message = isNew ? TeamMessages.AddOk:TeamMessages.UpdateOk;
            return response;
        }
        private async Task<Team> createOrGetExisting()
        {
            if (request.Id == 0)
            {
                isNew=true;
                var model = new Team();
                context.Teams.Add(model);
                return model;
            }
            else
            {
                return await context.Teams
                                    .FirstOrDefaultAsync(c=>c.Id == request.Id);
            }
        }
    }
}

