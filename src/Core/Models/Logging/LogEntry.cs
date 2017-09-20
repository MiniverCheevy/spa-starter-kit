using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Logging
{
    public class LogEntry
    {
        public DateTimeOffset CreationDate { get; set; }
        public string Log { get; set; }
        public string Origin { get; set; }
    }
}
