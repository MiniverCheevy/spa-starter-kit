using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models.Logging;
using Voodoo;

namespace Core.Infrastructure.Logging
{
    public interface ITraceLogger
    {
        void Log(string log);
        List<LogEntry> GetAllLogs(bool clear = true);
    }
    public class ConsoleTraceLogger:ITraceLogger
    {
        private List<LogEntry> logs = new List<LogEntry>();
        public void Log(string log)
        {
            logs.Add(new LogEntry { CreationDate = DateTime.UtcNow, Log = log });
            Console.WriteLine(log);

        }

        public List<LogEntry> GetAllLogs(bool clear = true)
        {
            var result = this.logs;
            if (clear)
                this.logs = new List<LogEntry>();
            return result;
        }
    }
}