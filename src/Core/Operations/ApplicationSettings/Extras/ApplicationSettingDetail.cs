using Core;
using Voodoo.Validation;
using Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Voodoo.Messages;
using Voodoo.Infrastructure.Notations;
namespace Core.Operations.ApplicationSettings.Extras
{
    public class ApplicationSettingDetail
    {
        [UI(IsHidden = true)]
        public int Id {get;set;}
        
        [StringLength(128, ErrorMessage=ApplicationSettingMessages.NameTooLong)]
        public string Name {get;set;}
        
        [Required(ErrorMessage = Constants.Messages.Required)]
        [StringLength(128, ErrorMessage=ApplicationSettingMessages.ValueTooLong)]
        public string Value {get;set;}
        
    }
}

