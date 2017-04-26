using System.Collections.Generic;

namespace Voodoo.CodeGeneration.Projects
{
    public interface IProject
    {
        void Save();
        string GeRootNamespace();
        string GetOutputPath();
        string GetOutputType();
        string GetAssemblyName();
        void AddItem(string visualStudioItemTypeNode, string pathToProject);
        bool Contains(string item);
        bool NeedsUpdating { get; }
    }
}