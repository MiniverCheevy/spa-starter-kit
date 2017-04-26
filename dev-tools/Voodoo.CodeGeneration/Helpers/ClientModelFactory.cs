using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voodoo.CodeGeneration.Helpers.ModelBuilders;
using Voodoo.CodeGeneration.Models.VisualStudio;
using Voodoo.CodeGeneration.Templates.PCL;

namespace Voodoo.CodeGeneration.Helpers
{
    public class ClientModelFactory
    {
        private readonly ProjectFacade logic;

        public ClientModelFactory(ProjectFacade logic)
        {
            this.logic = logic;
        }

        public List<Type> GetTypes()
        {
            var restBuilder = new RestBuilder(logic, null);
            var modelTypes = new List<Type>();
            foreach (var item in restBuilder.Resources)
            {
                foreach (var verb in item.Verbs)
                {
                    modelTypes.Add(verb.RequestType);
                    modelTypes.Add(verb.ResponseType);
                }
            }
            modelTypes.AddRange(logic.ClientTypes);
            return modelTypes.OrderBy(c => c.Name).ToList();
        }
    }
}