using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Fernweh.Core.Operations.Lists;
using Voodoo.Validation;

namespace Fernweh.Core.Operations.Users.Extras
{
    public class UserMessage
    {
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

        public string Roles { get; set; }
    }

    public class UserDetail
    {
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

        [Required(ErrorMessage = Constants.Messages.Required)]
        public bool LockoutEnabled { get; set; }

        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        [CollectionMustHaveAtLeastOneItem]
        public List<ListItem> Roles { get; set; } = new List<ListItem>();
    }
}