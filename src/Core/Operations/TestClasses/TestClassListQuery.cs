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
    [Rest(Verb.Get, RestResources.TestClassList)]
    public class TestClassListQuery:QueryAsync<TestClassListRequest,TestClassListResponse>
    {
        private DatabaseContext context;
        private IValidator validator = ValidationManager.GetDefaultValidatitor();
        public TestClassListQuery (TestClassListRequest request) : base(request)
        {
        }
        protected override async Task<TestClassListResponse> ProcessRequestAsync()
        {
            using (context = IOC.GetContext())
            {
                var query = context.TestClasses.AsNoTracking().AsQueryable();
                var data = await query.ToPagedResponseAsync(request, c => c.ToTestClassRow());
                response.From(data, c=>c);
            }
            return response;
        }
    }
}

