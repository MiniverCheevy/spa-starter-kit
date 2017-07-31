using Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Voodoo.Messages;
namespace Core.Operations.Projects.Extras
{
    public class ProjectListRequest : PagedRequest
    {
        public override string DefaultSortMember => "Name";
    }
}

