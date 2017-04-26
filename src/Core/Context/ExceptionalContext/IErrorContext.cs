using System.Data.Entity;

namespace Fernweh.Core.Context.ExceptionalContext
{
    public interface IErrorContext
    {
        DbSet<Error> Errors { get; set; }
    }
}