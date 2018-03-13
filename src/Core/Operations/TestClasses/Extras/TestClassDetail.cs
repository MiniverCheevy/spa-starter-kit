using Core;
using Voodoo.Validation;
using Voodoo.CodeGeneration.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Voodoo.Messages;
using Voodoo.Infrastructure.Notations;
namespace Core.Operations.TestClasses.Extras
{
    public class TestClassDetail
    {
        [UI(IsHidden = true)]
        public int Id {get;set;}
        
        [StringLength(128, ErrorMessage=TestClassMessages.NameTooLong)]
        public string Name {get;set;}
        
    }
}

