using Core;
using Core.Models.Scratch;
using Core.Operations.Projects.Extras;
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
namespace Core.Operations.Projects
{
    [Rest(Verb.Get, RestResources.ProjectList)]
    public class ProjectListQuery:QueryAsync<ProjectListRequest,ProjectListResponse>
    {
        private MainContext context;
        private IValidator validator = ValidationManager.GetDefaultValidatitor();
        public ProjectListQuery (ProjectListRequest request) : base(request)
        {
        }
        protected override async Task<ProjectListResponse> ProcessRequestAsync()
        {
            using (context = IOC.GetContext())
            {
                var query = context.Projects.AsNoTracking().AsQueryable();
                var data = await query.ToPagedResponseAsync(request, c => c.ToProjectRow());
                response.From(data, c=>c);
            }
            return response;
        }
    }
}
