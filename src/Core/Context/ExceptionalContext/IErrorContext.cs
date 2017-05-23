using System.Data.Entity;

namespace Core.Context.ExceptionalContext
{
    public interface IErrorContext
    {
        DbSet<Error> Errors { get; set; }
    }
}