using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Infrastructure.Logging;
using Core.Models.Logging;

namespace Tests.Fakes
{
    public class TestTraceLogger: ITraceLogger
    {

        private static List<LogEntry> logs = new List<LogEntry>();
        public void Log(string log)
        {
            var entry = new LogEntry { CreationDate = DateTime.UtcNow, Log = log };
            logs.Add(entry);
            Debug.WriteLine($"{entry.CreationDate.TimeOfDay.ToString().PadRight(20)} -> {log}");
        }

        public List<LogEntry> GetAllLogs(bool clear = true)
        {
            var result = logs;
            if (clear)
                logs = new List<LogEntry>();

            return result;
        }
    }
}
