using Voodoo.Messages;

namespace Core.Operations.Errors.Extras
{
    public class ErrorQueryRequest : PagedRequest
    {
        public string SearchText { get; set; }
        public override string DefaultSortMember => "CreationDate DESC";
    }
}