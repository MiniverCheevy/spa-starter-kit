using System.ComponentModel.DataAnnotations;

namespace Fernweh.Core.Operations.ApplicationSettings.Extras
{
    public class ApplicationSettingMessage
    {
        public int Id { get; set; }

        [StringLength(128, ErrorMessage = ApplicationSettingMessages.NameTooLong)]
        public string Name { get; set; }

        [Required(ErrorMessage = Constants.Messages.Required)]
        [StringLength(128, ErrorMessage = ApplicationSettingMessages.ValueTooLong)]
        public string Value { get; set; }
    }
}