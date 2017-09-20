using System.Collections.Generic;
using Core.Models.Logging;

namespace Core.Logging
{
    public interface ITraceLogger
    {
        void Log(string log);
        List<LogEntry> getAllLogs(bool clear = true);
    }
}