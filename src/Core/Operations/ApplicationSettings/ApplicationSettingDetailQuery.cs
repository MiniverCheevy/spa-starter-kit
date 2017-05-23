using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voodoo.Messages;
using Voodoo.Operations.Async;
using Voodoo.Infrastructure;
using Voodoo.Validation.Infrastructure;
using System.Data.Entity;
using Core;
using Core.Operations.ApplicationSettings.Extras;
using Core.Models;
using Core.Context;
namespace Core.Operations.ApplicationSettings
{
    [Rest(Verb.Get, RestResources.ApplicationSetting)]
    public class ApplicationSettingDetailQuery : QueryAsync<IdRequest,Response<ApplicationSettingMessage>>
    {
        private MainContext context;
        protected IValidator validator = ValidationManager.GetDefaultValidatitor();
        
        public ApplicationSettingDetailQuery (IdRequest request) : base(request)
        {
        }
        
        protected override async Task<Response<ApplicationSettingMessage>> ProcessRequestAsync()
        {
            var model = new ApplicationSetting();
            if (request.Id != 0)
            {
                using(context = IOC.GetContext())
                {
                    var query = context.ApplicationSettings.AsNoTracking().AsQueryable()
                                       .Where(c=>c.Id == request.Id);
                    
                    model = await query.FirstOrDefaultAsync();
                    if (model == null)
                    throw new Exception(ApplicationSettingMessages.NotFound);
                }
                
            }
            response.Data = model.ToApplicationSettingMessage();
            return response;
        }
    }
}

