using Core.Infrastructure.Logging;
using Core.Models.Logging;

namespace Core.Operations.Errors.Extras
{
    public class ErrorRequest
    {
        public IErrorFactory ErrorFactory { get; set; }
    }
}