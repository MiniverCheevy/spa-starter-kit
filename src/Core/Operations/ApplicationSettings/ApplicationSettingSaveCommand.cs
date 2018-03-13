
using Core;
using Core.Models;
using Core.Operations.ApplicationSettings.Extras;
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
using Microsoft.EntityFrameworkCore;
namespace Core.Operations.ApplicationSettings
{
    [Rest(Verb.Put, RestResources.ApplicationSetting)]
    public class ApplicationSettingSaveCommand :CommandAsync<ApplicationSettingDetail, NewItemResponse>
    {
        private bool isNew;
        private DatabaseContext context;
        private IValidator validator = ValidationManager.GetDefaultValidatitor();
        public ApplicationSettingSaveCommand(ApplicationSettingDetail request) : base(request)
        {
        }
        
        protected override async Task<NewItemResponse> ProcessRequestAsync()
        {
            //The request object is validated by default, validate anything else with
            //validator.Validate(<something>);
            
            using(context = IOC.GetContext())
            {
                var model = await createOrGetExisting();
                model.ThrowIfNull(ApplicationSettingMessages.NotFound);
                
                model.UpdateFrom(request);
                await context.SaveChangesAsync();
                
                response.NewItemId = model.Id;
            }
            response.Message = isNew ? ApplicationSettingMessages.AddOk:ApplicationSettingMessages.UpdateOk;
            return response;
        }
        private async Task<ApplicationSetting> createOrGetExisting()
        {
            if (request.Id == 0)
            {
                isNew=true;
                var model = new ApplicationSetting();
                context.ApplicationSettings.Add(model);
                return model;
            }
            else
            {
                return await context.ApplicationSettings
                                    .FirstOrDefaultAsync(c=>c.Id == request.Id);
            }
        }
    }
}

