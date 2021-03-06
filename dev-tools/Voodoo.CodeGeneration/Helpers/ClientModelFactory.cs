﻿using System;
using System.Collections.Generic;
using System.Linq;
using Voodoo.CodeGeneration.Models.VisualStudio;

namespace Voodoo.CodeGeneration.Helpers
{ 
    public class ClientModelFactory
    {
        private readonly ProjectFacade logic;
        private ProjectFacade models;

        public ClientModelFactory(ProjectFacade logic, ProjectFacade models)
        {
            this.logic = logic;
            this.models = models;
        }

        public List<Type> GetTypes()
        {
            var restBuilder = new RestBuilder(logic, null);
            var modelTypes = new List<Type>();
            foreach (var item in restBuilder.Resources)
            foreach (var verb in item.Verbs)
            {
                modelTypes.Add(verb.RequestType);
                modelTypes.Add(verb.ResponseType);
            }
            modelTypes.AddRange(logic.ClientTypes);
            modelTypes.AddRange(models.ClientTypes);
            return modelTypes.OrderBy(c => c.Name).ToList();
        }
    }
}