using Core;
using Core.Models.Scratch;
using Core.Context;
namespace Core.Operations.Projects.Extras
{
    public  static partial class ProjectExtensions
    {
        public static ProjectRepository ProjectRepository(this MainContext context)
        {
            return new ProjectRepository(context);
        }
        
        public static ProjectRow ToProjectRow(this Project model)
        {
            var message = toProjectRow(model, new ProjectRow());
            return message;
        }
        public static Project UpdateFrom(this  Project model, ProjectRow message)
        {
            return updateFromProjectRow(message, model);
            
        }
        public static ProjectDetail ToProjectDetail(this Project model)
        {
            var message = toProjectDetail(model, new ProjectDetail());
            return message;
        }
        public static Project UpdateFrom(this  Project model, ProjectDetail message)
        {
            return updateFromProjectDetail(message, model);
            
        }
        
    }
}

