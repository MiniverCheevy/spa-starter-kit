using System;
using System.Collections.Generic;

namespace Voodoo.CodeGeneration.Models.Rest
{
    [Serializable]
    public class Resource
    {
        public string ClassName { get; set; }
        public List<RestMethod> Verbs { get; set; }
        public string Name { get; set; }

        public Resource()
        {
            Verbs = new List<RestMethod>();
        }
    }
}