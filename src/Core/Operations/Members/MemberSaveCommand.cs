
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
using Microsoft.EntityFrameworkCore;
namespace Core.Operations.Members
{
    [Rest(Verb.Put, RestResources.Member)]
    public class MemberSaveCommand :CommandAsync<MemberDetail, NewItemResponse>
    {
        private bool isNew;
        private DatabaseContext context;
        private IValidator validator = ValidationManager.GetDefaultValidatitor();
        public MemberSaveCommand(MemberDetail request) : base(request)
        {
        }
        
        protected override async Task<NewItemResponse> ProcessRequestAsync()
        {
            //The request object is validated by default, validate anything else with
            //validator.Validate(<something>);
            
            using(context = IOC.GetContext())
            {
                var model = await createOrGetExisting();
                model.ThrowIfNull(MemberMessages.NotFound);
                
                model.UpdateFrom(request);
                await context.SaveChangesAsync();
                
                response.NewItemId = model.Id;
            }
            response.Message = isNew ? MemberMessages.AddOk:MemberMessages.UpdateOk;
            return response;
        }
        private async Task<Member> createOrGetExisting()
        {
            if (request.Id == 0)
            {
                isNew=true;
                var model = new Member();
                context.Members.Add(model);
                return model;
            }
            else
            {
                return await context.Members
                                    .FirstOrDefaultAsync(c=>c.Id == request.Id);
            }
        }
    }
}

