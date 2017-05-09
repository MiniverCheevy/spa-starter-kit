using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace React.Infrastructure.ExecutionPipeline.Models
{
    public class SecurityContext
    {
        public bool AllowAnonymouse { get; set; }
        public string[] Roles { get; set; }
    }
}
