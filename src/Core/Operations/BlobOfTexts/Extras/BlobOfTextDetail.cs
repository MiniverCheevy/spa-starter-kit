using Core;
using Voodoo.Validation;
using Core.Models.Scratch;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Voodoo.Messages;
using Voodoo.Infrastructure.Notations;
namespace Core.Operations.BlobOfTexts.Extras
{
    public class BlobOfTextDetail
    {
        [UI(IsHidden = true)]
        public int Id {get;set;}
        
        [Required(ErrorMessage = Constants.Messages.Required)]
        [StringLength(128, ErrorMessage=BlobOfTextMessages.TextTooLong)]
        public string Text {get;set;}
        
        [Display(Name = "Member")]
        [RequiredNonZeroInt(ErrorMessage = Constants.Messages.Required)]
        public int MemberId {get;set;}
        
    }
}

