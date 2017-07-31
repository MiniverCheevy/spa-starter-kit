
using Core;
using Core.Models.Scratch;
using Core.Operations.Projects.Extras;
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
namespace Core.Operations.Projects
{
    [Rest(Verb.Put, RestResources.Project)]
    public class ProjectSaveCommand :CommandAsync<ProjectDetail, NewItemResponse>
    {
        private bool isNew;
        private MainContext context;
        private IValidator validator = ValidationManager.GetDefaultValidatitor();
        public ProjectSaveCommand(ProjectDetail request) : base(request)
        {
        }
        
        protected override async Task<NewItemResponse> ProcessRequestAsync()
        {
            //The request object is validated by default, validate anything else with
            //validator.Validate(<something>);
            
            using(context = IOC.GetContext())
            {
                var model = await createOrGetExisting();
                model.ThrowIfNull(ProjectMessages.NotFound);
                
                model.UpdateFrom(request);
                await context.SaveChangesAsync();
                
                response.NewItemId = model.Id;
            }
            response.Message = isNew ? ProjectMessages.AddOk:ProjectMessages.UpdateOk;
            return response;
        }
        private async Task<Project> createOrGetExisting()
        {
            if (request.Id == 0)
            {
                isNew=true;
                var model = new Project();
                context.Projects.Add(model);
                return model;
            }
            else
            {
                return await context.Projects
                                    .FirstOrDefaultAsync(c=>c.Id == request.Id);
            }
        }
    }
}

