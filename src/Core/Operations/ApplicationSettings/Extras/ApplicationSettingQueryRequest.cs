using Voodoo.Messages;

namespace Core.Operations.ApplicationSettings.Extras
{
    public class ApplicationSettingQueryRequest : PagedRequest
    {
        public override string DefaultSortMember => "Name";
    }
}