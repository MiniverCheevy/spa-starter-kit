using Voodoo.Messages;

namespace Fernweh.Core.Operations.Roles.Extras
{
    public class RoleQueryRequest : PagedRequest
    {
        public override string DefaultSortMember { get; }
    }
}