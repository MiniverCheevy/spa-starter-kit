using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Voodoo.Messages;

namespace Core.Operations.Errors.Extras
{
    public class ErrorMessage
    {
        [Required(ErrorMessage = Constants.Messages.Required)]
        public long Id { get; set; }

        [Display(Name = "Creation Date")]
        [Required(ErrorMessage = Constants.Messages.Required)]
        [Range(typeof(DateTime), "1/1/1900", "3/4/2050", ErrorMessage = Constants.Messages.DateOutOfRange)]
        public DateTime CreationDate { get; set; }

        [StringLength(200, ErrorMessage = ErrorMessages.TypeTooLong)]
        public string Type { get; set; }

        [StringLength(200, ErrorMessage = ErrorMessages.TypeTooLong)]
        public string Message { get; set; }

        public string User { get; set; }
    }

    public class ErrorDetail : ErrorMessage
    {
        public string Details { get; set; }
        public string Host { get; set; }

        public string Url { get; set; }


        public List<Grouping<NameValuePair>> Items { get; set; }

        public ErrorDetail()
        {
            Items = new List<Grouping<NameValuePair>>();
        }
    }
}