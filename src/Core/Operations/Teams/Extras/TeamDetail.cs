using Core;
using Voodoo.Validation;
using Core.Models.Scratch;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Voodoo.Messages;
using Voodoo.Infrastructure.Notations;
namespace Core.Operations.Teams.Extras
{
    public class TeamDetail
    {
        [UI(IsHidden = true)]
        public int Id {get;set;}
        
        [StringLength(128, ErrorMessage=TeamMessages.NameTooLong)]
        public string Name {get;set;}
        
    }
}

