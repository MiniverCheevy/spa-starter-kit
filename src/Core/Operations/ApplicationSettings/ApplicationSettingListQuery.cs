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
using Core.Context;
using Voodoo;
namespace Core.Operations.ApplicationSettings
{
    [Rest(Verb.Get, RestResources.ApplicationSettingList)]
    public class ApplicationSettingListQuery : QueryAsync<ApplicationSettingQueryRequest,ApplicationSettingQueryResponse>
    {
        private MainContext context;
        protected IValidator validator = ValidationManager.GetDefaultValidatitor();
        
        public ApplicationSettingListQuery (ApplicationSettingQueryRequest request) : base(request)
        {
        }
        
        protected override async Task<ApplicationSettingQueryResponse> ProcessRequestAsync()
        {
            using(context = IOC.GetContext())
            {
                var query =context.ApplicationSettings.AsNoTracking().AsQueryable();
                var data = await query.ToPagedResponseAsync(request, c => c.ToApplicationSettingMessage());
                response.From(data, c=>c);
                //Note: complex operations in the mapping may result in an error you can replace it with
                
                //var res = await query.ToPagedResponseAsync(request);
                //response.From(res, c=> helper.Map(c));
                
                //the first method is preferred as it limits the data returned from the server
                
            }
            
            return response;
        }
        
    }
}

