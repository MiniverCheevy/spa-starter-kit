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
using Fernweh.Core;
using Fernweh.Core.Operations.ApplicationSettings.Extras;
using Fernweh.Core.Models;
using Fernweh.Core.Context;
namespace Fernweh.Core.Operations.ApplicationSettings
{
    [Rest(Verb.Get, RestResources.ApplicationSetting)]
    public class ApplicationSettingDetailQuery : QueryAsync<IdRequest,Response<ApplicationSettingMessage>>
    {
        private FernwehContext context;
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

