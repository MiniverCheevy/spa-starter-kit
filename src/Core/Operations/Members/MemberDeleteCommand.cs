
using Core;
using Core.Models.Scratch;
using Core.Operations.Members.Extras;
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
namespace Core.Operations.Members
{
    [Rest(Verb.Delete, RestResources.Member)]
    public class MemberDeleteCommand :CommandAsync<IdRequest,Response>
    {
        private DatabaseContext context;
        private IValidator validator = ValidationManager.GetDefaultValidatitor();
        
        public MemberDeleteCommand(IdRequest request) : base(request)
        {
        }
        
        protected override async Task<Response> ProcessRequestAsync()
        {
            using(context = IOC.GetContext()){
            var model = await context.Members
                                     .FirstOrDefaultAsync(c=>c.Id == request.Id);
            
            if (model == null)
            throw new Exception(MemberMessages.NotFound);
            
            context.Members.Remove(model);
            response.NumberOfRowsEffected = await context.SaveChangesAsync();
        }
        response.Message = MemberMessages.DeleteOk;
        return response;
    }
}
}

