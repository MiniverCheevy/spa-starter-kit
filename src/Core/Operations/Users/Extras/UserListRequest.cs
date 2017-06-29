using Voodoo.Messages;

namespace Core.Operations.Users.Extras
{
    public class UserListRequest : PagedRequest
    {
        public string SearchText { get; set; }

        public override string DefaultSortMember => "UserName";

        public int Id { get; set; }
    }
}