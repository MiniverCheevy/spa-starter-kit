using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Operations.Errors.Extras
{
    public class ErrorRow
    {
        [Required(ErrorMessage = Constants.Messages.Required)]
        public long Id { get; set; }

        [Display(Name = "Creation Date")]
        [Required(ErrorMessage = Constants.Messages.Required)]
        [Range(typeof(DateTime), "1/1/1900", "3/4/2050", ErrorMessage = Constants.Messages.DateOutOfRange)]
        public DateTimeOffset CreationDate { get; set; }

        [StringLength(200, ErrorMessage = ErrorMessages.TypeTooLong)]
        public string Type { get; set; }

        [StringLength(200, ErrorMessage = ErrorMessages.TypeTooLong)]
        public string Message { get; set; }

        public string User { get; set; }
    }
}