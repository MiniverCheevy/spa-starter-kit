using Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Voodoo.Messages;
namespace Core.Operations.ApplicationSettings.Extras
{
    public class ApplicationSettingListRequest : PagedRequest
    {
        public override string DefaultSortMember => "Name";
    }
}

