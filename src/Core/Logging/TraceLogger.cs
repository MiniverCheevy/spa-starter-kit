using System.Diagnostics;

namespace Fernweh.Core.Logging
{
    public class VoidTraceWriter : ITraceLogger
    {
        public void Log(string message)
        {
        }

        public IScopedLogger GetScopedLogger(string scope)
        {
            return new VoidLogger();
        }
    }

    public interface ITraceLogger
    {
        void Log(string message);
        IScopedLogger GetScopedLogger(string scope);
    }

    public interface IScopedLogger
    {
        void Write(string message);
    }

    public class VoidLogger : IScopedLogger
    {
        public void Write(string message)
        {
        }
    }


    public class TraceLogger : IScopedLogger
    {
        private Stopwatch stopwatch;
        private string scope;

        public TraceLogger(string scope)
        {
            this.scope = scope.PadRight(16);
            if (this.scope.Length > 16)
                this.scope = this.scope.Substring(0, 16);
            this.stopwatch = Stopwatch.StartNew();
        }

        public void Write(string message)
        {
            var time = $"{stopwatch.ElapsedMilliseconds}";
            time.PadRight(16);
            message = $"{scope} {time} {message}";
            IOC.TraceWriter.Log(message);
        }
    }
}