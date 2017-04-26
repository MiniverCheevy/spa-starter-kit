using System.ComponentModel.DataAnnotations;

namespace Fernweh.Core.Operations.Roles.Extras
{
    public class RoleMessage
    {
        public int Id { get; set; }

        [StringLength(128, ErrorMessage = RoleMessages.NameTooLong)]
        public string Name { get; set; }
    }
}