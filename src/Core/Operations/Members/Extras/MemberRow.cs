using Core;
using Voodoo.Validation;
using Core.Models.Scratch;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Voodoo.Messages;
using Voodoo.Infrastructure.Notations;
namespace Core.Operations.Members.Extras
{
    public class MemberRow
    {
        [UI(IsHidden = true)]
        public int Id {get;set;}
        
        [StringLength(128, ErrorMessage=MemberMessages.NameTooLong)]
        public string Name {get;set;}
        
        [Required(ErrorMessage = Constants.Messages.Required)]
        [StringLength(128, ErrorMessage=MemberMessages.TitleTooLong)]
        public string Title {get;set;}
        
        [Display(Name = "Required Int")]
        [RequiredNonZeroInt(ErrorMessage = Constants.Messages.Required)]
        public int RequiredInt {get;set;}
        
        [Display(Name = "Optional Int")]
        public int? OptionalInt {get;set;}
        
    }
}

