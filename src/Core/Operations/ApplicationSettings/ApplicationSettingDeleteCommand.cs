using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Fernweh.Core.Context;
using Fernweh.Core.Operations.ApplicationSettings.Extras;
using Voodoo.Infrastructure;
using Voodoo.Messages;
using Voodoo.Operations.Async;
using Voodoo.Validation.Infrastructure;

namespace Fernweh.Core.Operations.ApplicationSettings
{
    [Rest(Verb.Delete, RestResources.ApplicationSetting)]
    public class ApplicationSettingDeleteCommand : CommandAsync<IdRequest, Response>
    {
        protected Context.AppContext context;
        protected IValidator validator = ValidationManager.GetDefaultValidatitor();

        public ApplicationSettingDeleteCommand(IdRequest request) : base(request)
        {
        }

        protected override async Task<Response> ProcessRequestAsync()
        {
            using (context = IOC.GetContext())
            {
                var model = await context.ApplicationSettings
                    .FirstOrDefaultAsync(c => c.Id == request.Id);

                if (model == null)
                    throw new Exception(ApplicationSettingMessages.NotFound);

                context.ApplicationSettings.Remove(model);
                response.NumberOfRowsEffected = await context.SaveChangesAsync();
            }
            response.Message = ApplicationSettingMessages.DeleteOk;
            return response;
        }
    }
}