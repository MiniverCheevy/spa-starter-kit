using System.ComponentModel.DataAnnotations;
using Voodoo.Infrastructure.Notations;

namespace Core.Operations.Users.Extras
{
    public class UserRow
    {
        [UI(IsHidden = true)]
        public int Id { get; set; }

        [Required(ErrorMessage = Constants.Messages.Required)]
        [StringLength(128, ErrorMessage = UserMessages.UserNameTooLong)]
        [EmailAddress]
        public string UserName { get; set; }

        public int? ClientId { get; set; }

        [Required(ErrorMessage = Constants.Messages.Required)]
        [StringLength(128, ErrorMessage = UserMessages.FirstNameTooLong)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = Constants.Messages.Required)]
        [StringLength(128, ErrorMessage = UserMessages.LastNameTooLong)]
        public string LastName { get; set; }

        [UI(DoNotSort =true)]
        public string Roles { get; set; }
    }
}