
using Core;
using Core.Models.Scratch;
using Core.Operations.Projects.Extras;
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
namespace Core.Operations.Projects
{
    [Rest(Verb.Get, RestResources.Project)]
    public class ProjectDetailQuery : QueryAsync<IdRequest,Response<ProjectDetail>>
    {
        private DatabaseContext context;
        private IValidator validator = ValidationManager.GetDefaultValidatitor();
        
        public ProjectDetailQuery (IdRequest request) : base(request)
        {
        }
        
        protected override async Task<Response<ProjectDetail>> ProcessRequestAsync()
        {
            var model = new Project();
            if (request.Id != 0)
            {
                using(context = IOC.GetContext())
                {
                    var query = context.Projects.AsNoTracking().AsQueryable()
                                       .Where(c=>c.Id == request.Id);
                    
                    model = await query.FirstOrDefaultAsync();
                    if (model == null)
                    throw new Exception(ProjectMessages.NotFound);
                }
                
            }
            response.Data = model.ToProjectDetail();
            return response;
        }
    }
}

