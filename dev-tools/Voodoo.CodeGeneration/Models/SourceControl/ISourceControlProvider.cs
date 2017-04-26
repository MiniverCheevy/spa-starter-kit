namespace Voodoo.CodeGeneration.Models.SourceControl
{
    public interface ISourceControlProvider
    {
        bool IsActive { get; set; }
        void CheckOutFiles(params string[] paths);
        void AddFiles(params string[] paths);
    }
}