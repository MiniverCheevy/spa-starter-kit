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
    public class MemberDetail
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
        
        [Display(Name = "Required Date")]
        [Required(ErrorMessage = Constants.Messages.Required)]
        [Range(typeof(DateTime), "1/1/1900", "3/4/2050", ErrorMessage = Constants.Messages.DateOutOfRange)]
        public DateTime RequiredDate {get;set;}
        
        [Display(Name = "Optional Date")]
        [Range(typeof(DateTime), "1/1/1900", "3/4/2050", ErrorMessage = Constants.Messages.DateOutOfRange)]
        public DateTime? OptionalDate {get;set;}
        
        [Display(Name = "Required Decimal")]
        [Required(ErrorMessage = Constants.Messages.Required)]
        public decimal RequiredDecimal {get;set;}
        
        [Display(Name = "Optional Decimal")]
        public decimal? OptionalDecimal {get;set;}
        
        [Display(Name = "Manager")]
        public int? ManagerId {get;set;}
        
    }
}

