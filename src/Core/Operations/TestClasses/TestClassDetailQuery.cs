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
    [Rest(Verb.Get, RestResources.TestClass)]
    public class TestClassDetailQuery :QueryAsync<IdRequest,Response<TestClassDetail>>
    {
        private DatabaseContext context;
        private IValidator validator = ValidationManager.GetDefaultValidatitor();
        public TestClassDetailQuery(IdRequest request) : base(request)
        {
        }
        protected override async Task<Response<TestClassDetail>> ProcessRequestAsync()
        {
            using(context = IOC.GetContext())
            {
                var model = await context.TestClasses
                                         .FirstOrDefaultAsync(c=>c.Id == request.Id);
                model.ThrowIfNull(TestClassMessages.NotFound);
                response.Data=model.ToTestClassDetail();
            }
            return response;
        }
    }
}

