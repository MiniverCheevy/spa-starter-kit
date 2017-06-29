
using Core;
using Core.Models;
using Core.Operations.ApplicationSettings.Extras;
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
namespace Core.Operations.ApplicationSettings
{
    [Rest(Verb.Delete, RestResources.ApplicationSetting)]
    public class ApplicationSettingDeleteCommand :CommandAsync<IdRequest,Response>
    {
        private MainContext context;
        private IValidator validator = ValidationManager.GetDefaultValidatitor();
        
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

