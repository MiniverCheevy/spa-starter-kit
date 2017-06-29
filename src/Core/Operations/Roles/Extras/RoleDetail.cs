using Core;
using Voodoo.Validation;
using Core.Models.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Voodoo.Messages;
using Voodoo.Infrastructure.Notations;
namespace Core.Operations.Roles.Extras
{
    public class RoleDetail
    {
        [UI(IsHidden = true)]
        public int Id {get;set;}
        
        [StringLength(128, ErrorMessage=RoleMessages.NameTooLong)]
        public string Name {get;set;}
        
    }
}

