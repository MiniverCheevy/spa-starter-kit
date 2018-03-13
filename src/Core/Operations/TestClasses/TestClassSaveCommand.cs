using Core;
using Voodoo.CodeGeneration.Models;
using Core.Operations.TestClasses.Extras;
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
namespace Core.Operations.TestClasses
{
    [Rest(Verb.Post, RestResources.TestClass)]
    public class TestClassSaveCommand :CommandAsync<TestClassDetail,NewItemResponse>
    {
        private DatabaseContext context;
        private bool isNew = false;
        private IValidator validator = ValidationManager.GetDefaultValidatitor();
        public TestClassSaveCommand(TestClassDetail request) : base(request)
        {
        }
        protected override async Task<NewItemResponse> ProcessRequestAsync()
        {
            using(context = IOC.GetContext())
            {
                var model = await createOrGetExisting();
                model.ThrowIfNull(TestClassMessages.NotFound);
                model.UpdateFrom(request);
                await context.SaveChangesAsync();
                
                response.NewItemId = model.Id;
                
            }
            response.Message =
            isNew ? TestClassMessages.AddOk:TestClassMessages.UpdateOk;
            return response;
        }
        protected async Task<TestClass> createOrGetExisting()
        {
            if (request.Id == 0)
            {
                isNew=true;
                var model = new TestClass();
                context.TestClasses.Add(model);
                return model;
            }
            else
            {
                return await context.TestClasses
                                    .FirstOrDefaultAsync(c=>c.Id == request.Id);
            }
        }
    }
}

