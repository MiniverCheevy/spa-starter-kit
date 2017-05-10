namespace Voodoo.CodeGeneration.Projects
{
    public interface IProject
    {
        bool NeedsUpdating { get; }
        void Save();
        string GeRootNamespace();
        string GetOutputPath();
        string GetOutputType();
        string GetAssemblyName();
        void AddItem(string visualStudioItemTypeNode, string pathToProject);
        bool Contains(string item);
    }
}