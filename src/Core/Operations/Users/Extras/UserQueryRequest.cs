using Voodoo.Messages;

namespace Core.Operations.Users.Extras
{
    public class UserQueryRequest : PagedRequest
    {
        public string SearchText { get; set; }
        public int? ClientId { get; set; }

        public override string DefaultSortMember => "UserName";

        public int Id { get; set; }
    }
}