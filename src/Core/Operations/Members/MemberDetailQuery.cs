
using Core;
using Core.Models.Scratch;
using Core.Operations.Members.Extras;
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
namespace Core.Operations.Members
{
    [Rest(Verb.Get, RestResources.Member)]
    public class MemberDetailQuery : QueryAsync<IdRequest,Response<MemberDetail>>
    {
        private MainContext context;
        private IValidator validator = ValidationManager.GetDefaultValidatitor();
        
        public MemberDetailQuery (IdRequest request) : base(request)
        {
        }
        
        protected override async Task<Response<MemberDetail>> ProcessRequestAsync()
        {
            var model = new Member();
            if (request.Id != 0)
            {
                using(context = IOC.GetContext())
                {
                    var query = context.Members.AsNoTracking().AsQueryable()
                                       .Where(c=>c.Id == request.Id);
                    
                    model = await query.FirstOrDefaultAsync();
                    if (model == null)
                    throw new Exception(MemberMessages.NotFound);
                }
                
            }
            response.Data = model.ToMemberDetail();
            return response;
        }
    }
}

