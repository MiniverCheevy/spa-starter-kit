using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Voodoo.Messages;
using Voodoo.Operations;
using Voodoo.Operations.Async;
using Voodoo.Validation.Infrastructure;
using System.Data.Entity;
using Core;
using Core.Context;
using Core.Models;
using Voodoo.Infrastructure;
using Core.Operations.ApplicationSettings.Extras;
namespace Core.Operations.ApplicationSettings
{
    [Rest(Verb.Delete, RestResources.ApplicationSetting)]
    public class ApplicationSettingDeleteCommand :CommandAsync<IdRequest,Response>
    {
        protected MainContext context;
        protected IValidator validator = ValidationManager.GetDefaultValidatitor();
        
        public ApplicationSettingDeleteCommand(IdRequest request) : base(request)
        {
        }
        
        protected override async Task<Response> ProcessRequestAsync()
        {
            using(context = IOC.GetContext()){
            var model = await context.ApplicationSettings
                                     .FirstOrDefaultAsync(c=>c.Id == request.Id);
            
            if (model == null)
            throw new Exception(ApplicationSettingMessages.NotFound);
            
            context.ApplicationSettings.Remove(model);
            response.NumberOfRowsEffected = await context.SaveChangesAsync();
        }
        response.Message = ApplicationSettingMessages.DeleteOk;
        return response;
    }
}
}

