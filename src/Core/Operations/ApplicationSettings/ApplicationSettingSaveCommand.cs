using System.Data.Entity;
using System.Threading.Tasks;
using Fernweh.Core.Context;
using Fernweh.Core.Models;
using Fernweh.Core.Operations.ApplicationSettings.Extras;
using Voodoo;
using Voodoo.Infrastructure;
using Voodoo.Messages;
using Voodoo.Operations.Async;
using Voodoo.Validation.Infrastructure;

namespace Fernweh.Core.Operations.ApplicationSettings
{
    [Rest(Verb.Put, RestResources.ApplicationSettingDetail)]
    public class ApplicationSettingSaveCommand : CommandAsync<ApplicationSettingMessage, NewItemResponse>
    {
        protected MainContext context;
        protected bool isNew;
        protected IValidator validator = ValidationManager.GetDefaultValidatitor();

        public ApplicationSettingSaveCommand(ApplicationSettingMessage request) : base(request)
        {
        }

        protected override async Task<NewItemResponse> ProcessRequestAsync()
        {
            //The request object is validated by default, validate anything else with
            //validator.Validate(<something>);

            using (context = IOC.GetContext())
            {
                var model = await createOrGetExisting();
                model.ThrowIfNull(ApplicationSettingMessages.NotFound);

                model.UpdateFrom(request);
                await context.SaveChangesAsync();

                response.NewItemId = model.Id;
            }
            response.Message = isNew ? ApplicationSettingMessages.AddOk : ApplicationSettingMessages.UpdateOk;
            return response;
        }

        protected async Task<ApplicationSetting> createOrGetExisting()
        {
            if (request.Id == 0)
            {
                isNew = true;
                var model = new ApplicationSetting();
                context.ApplicationSettings.Add(model);
                return model;
            }
            return await context.ApplicationSettings
                .FirstOrDefaultAsync(c => c.Id == request.Id);
        }
    }
}