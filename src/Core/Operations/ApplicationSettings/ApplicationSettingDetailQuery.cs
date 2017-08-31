
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
using System.Data.Entity;
namespace Core.Operations.ApplicationSettings
{
    [Rest(Verb.Get, RestResources.ApplicationSetting)]
    public class ApplicationSettingDetailQuery : QueryAsync<IdRequest,Response<ApplicationSettingDetail>>
    {
        private DatabaseContext context;
        private IValidator validator = ValidationManager.GetDefaultValidatitor();
        
        public ApplicationSettingDetailQuery (IdRequest request) : base(request)
        {
        }
        
        protected override async Task<Response<ApplicationSettingDetail>> ProcessRequestAsync()
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
            response.Data = model.ToApplicationSettingDetail();
            return response;
        }
    }
}

