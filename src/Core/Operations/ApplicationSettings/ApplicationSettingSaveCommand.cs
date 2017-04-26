using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Voodoo;
using Voodoo.Messages;
using Voodoo.Operations;
using Voodoo.Operations.Async;
using Voodoo.Validation.Infrastructure;
using System.Data.Entity;
using Fernweh.Core;
using Fernweh.Core.Context;
using Fernweh.Core.Models;
using Voodoo.Infrastructure;
using Fernweh.Core.Operations.ApplicationSettings.Extras;
namespace Fernweh.Core.Operations.ApplicationSettings
{
    [Rest(Verb.Put, RestResources.ApplicationSettingDetail)]
    public class ApplicationSettingSaveCommand :CommandAsync<ApplicationSettingMessage, NewItemResponse>
    {
        protected bool isNew;
        protected FernwehContext context;
        protected IValidator validator = ValidationManager.GetDefaultValidatitor();
        public ApplicationSettingSaveCommand(ApplicationSettingMessage request) : base(request)
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
        protected async Task<ApplicationSetting> createOrGetExisting()
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

