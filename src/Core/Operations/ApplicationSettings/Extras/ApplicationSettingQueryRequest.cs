using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Voodoo.Messages;
using Fernweh.Core;

namespace Fernweh.Core.Operations.ApplicationSettings.Extras
{
    public class ApplicationSettingQueryRequest : PagedRequest
    {
        public override string DefaultSortMember
        {
            get {
            return "Name";
            
        }
    }
}
}
