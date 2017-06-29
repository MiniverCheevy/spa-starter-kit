using Voodoo.Messages;

namespace Core.Operations.Errors.Extras
{
    public class ErrorListRequest : PagedRequest
    {
        public string SearchText { get; set; }
        public override string DefaultSortMember => "CreationDate DESC";
    }
}