using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    [Rest(Verb.Delete, RestResources.ApplicationSetting)]
    public class ApplicationSettingDeleteCommand :CommandAsync<IdRequest,Response>
    {
        protected FernwehContext context;
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

