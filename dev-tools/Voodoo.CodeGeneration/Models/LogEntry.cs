using System.Linq;
using Voodoo.Logging;

namespace Voodoo.CodeGeneration.Models
{
    public class LogEntry
    {
        public string Message { get; set; }
        public LogLevels Level { get; set; }

        private static string Format(string[] message)
        {
            return message.Length == 1
                ? message.FirstOrDefault()
                : string.Format(message.First(), message.Skip(1).ToArray().To<object[]>());
        }

        public static LogEntry Error(params string[] message)
        {
            return new LogEntry {Level = LogLevels.Error, Message = Format(message)};
        }

        public static LogEntry Info(params string[] message)
        {
            return new LogEntry {Level = LogLevels.Info, Message = Format(message)};
        }

        public static LogEntry Trace(params string[] message)
        {
            return new LogEntry {Level = LogLevels.Info, Message = Format(message)};
        }

        public override string ToString()
        {
            return Message;
        }
    }
}