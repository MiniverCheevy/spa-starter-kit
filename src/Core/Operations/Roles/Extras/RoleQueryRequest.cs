using Voodoo.Messages;

namespace Core.Operations.Roles.Extras
{
    public class RoleQueryRequest : PagedRequest
    {
        public override string DefaultSortMember { get; }
    }
}