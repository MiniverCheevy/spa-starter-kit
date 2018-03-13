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
    [Rest(Verb.Get, RestResources.ApplicationSettingList)]
    public class ApplicationSettingListQuery:QueryAsync<ApplicationSettingListRequest,ApplicationSettingListResponse>
    {
        private DatabaseContext context;
        private IValidator validator = ValidationManager.GetDefaultValidatitor();
        public ApplicationSettingListQuery (ApplicationSettingListRequest request) : base(request)
        {
        }
        protected override async Task<ApplicationSettingListResponse> ProcessRequestAsync()
        {
            using (context = IOC.GetContext())
            {
                var query = context.ApplicationSettings.AsNoTracking().AsQueryable();
                var data = await query.ToPagedResponseAsync(request, c => c.ToApplicationSettingRow());
                response.From(data, c=>c);
            }
            return response;
        }
    }
}

