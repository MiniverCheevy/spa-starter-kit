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
    [Rest(Verb.Delete, RestResources.TestClass)]
    public class TestClassDeleteCommand :CommandAsync<IdRequest,Response>
    {private DatabaseContext context;
    private IValidator validator = ValidationManager.GetDefaultValidatitor();
    public TestClassDeleteCommand(IdRequest request) : base(request)
    {
    }
    protected override async Task<Response> ProcessRequestAsync()
    {
        using(context = IOC.GetContext())
        {
            var model = await context.TestClasses
                                     .FirstOrDefaultAsync(c=>c.Id == request.Id);
            model.ThrowIfNull(TestClassMessages.NotFound);
            context.TestClasses.Remove(model);
            response.NumberOfRowsEffected = await context.SaveChangesAsync();
        }
        response.Message = TestClassMessages.DeleteOk;
        return response;
    }}
}

