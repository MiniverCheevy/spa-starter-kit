using Voodoo.CodeGeneration.Models;

namespace Voodoo.CodeGeneration.Projects
{
    public interface IProject
    {
        IProject GetProject(ProjectRef project);

        string GetFullAsseblyPath();
        string GeRootNamespace();        
        string GetAssemblyName();        
        bool Contains(string item);
        string GetFolder();
    }
}