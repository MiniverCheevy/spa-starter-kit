using Core;
using Voodoo.Validation;
using Core.Models.Scratch;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Voodoo.Messages;
using Voodoo.Infrastructure.Notations;
namespace Core.Operations.Projects.Extras
{
    public class ProjectRow
    {
        [UI(IsHidden = true)]
        public int Id {get;set;}
        
        [StringLength(128, ErrorMessage=ProjectMessages.NameTooLong)]
        public string Name {get;set;}
        
        [Display(Name = "Team")]
        [RequiredNonZeroInt(ErrorMessage = Constants.Messages.Required)]
        public int TeamId {get;set;}
        
    }
}

