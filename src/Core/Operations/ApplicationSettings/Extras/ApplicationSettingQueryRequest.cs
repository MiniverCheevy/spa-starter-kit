using Voodoo.Messages;

namespace Fernweh.Core.Operations.ApplicationSettings.Extras
{
    public class ApplicationSettingQueryRequest : PagedRequest
    {
        public override string DefaultSortMember => "Name";
    }
}