using System;
using System.Collections.Generic;
using System.Linq;
using Voodoo.CodeGeneration.Models.Rest;
using Voodoo.CodeGeneration.Models.VisualStudio;
using Voodoo.Infrastructure;

namespace Voodoo.CodeGeneration.Helpers
{
    public class RestBuilder
    {
        private ProjectFacade web;

        public List<Resource> Resources { get; set; }

        public Dictionary<Verb, RestMethod> Methods => Vs.Helper.Solution.WebIsAspDotNetCore.To<bool>()
            ? new Dictionary<Verb, RestMethod>
            {
                {Verb.Get, new RestMethod {Attribute = "[HttpGet]", Name = "Get", Parameter = ""}},
                {Verb.Post, new RestMethod {Attribute = "[HttpPost]", Name = "Post", Parameter = "[FromBody]"}},
                {Verb.Put, new RestMethod {Attribute = "[HttpPut]", Name = "Put", Parameter = "[FromBody]"}},
                {Verb.Delete, new RestMethod {Attribute = "[HttpDelete]", Name = "Delete", Parameter = ""}}
            }
            : new Dictionary<Verb, RestMethod>
            {
                {Verb.Get, new RestMethod {Attribute = "[HttpGet]", Name = "Get", Parameter = "[FromUri]"}},
                {Verb.Post, new RestMethod {Attribute = "[HttpPost]", Name = "Post", Parameter = "[FromBody]"}},
                {Verb.Put, new RestMethod {Attribute = "[HttpPut]", Name = "Put", Parameter = "[FromBody]"}},
                {Verb.Delete, new RestMethod {Attribute = "[HttpDelete]", Name = "Delete", Parameter = "[FromUri]"}}
            };

        public RestBuilder(ProjectFacade logic, ProjectFacade web)
        {
            this.web = web;
            Resources = new List<Resource>();
            var assembly = logic.Assembly;
            var types = assembly.GetTypesSafetly();
            var interestingTypes =
                types.Where(c => c.GetCustomAttributes(typeof(RestAttribute), false).Any()).ToArray();

            var resources =
                interestingTypes.ToLookup(
                    c => c.GetCustomAttributes(typeof(RestAttribute), false).First().To<RestAttribute>().Resource,
                    c => buildVerb(c.GetCustomAttributes(typeof(RestAttribute), false).First(), c));
            foreach (var key in resources.Select(c => c.Key).ToArray())
            {
                var name = key;
                var resource = new Resource
                {
                    Name = name,
                    ClassName = $"{name}Controller"
                };
                resource.Verbs.AddRange(resources[key]);
                Resources.Add(resource);
            }
        }

        private RestMethod buildVerb(object attributeObject, Type operationType)
        {
            var attribute = attributeObject.To<RestAttribute>();
            var method = GetRestMethod(attribute, operationType);
            return method;
        }

        public RestMethod GetRestMethod(RestAttribute attribute, Type operationType)
        {
            var verb = Methods[attribute.Verb];
            verb.AllowAnonymous = attribute.AllowAnonymous;
            verb.Roles = attribute.Roles;

            Operation.DiscoverTypes(operationType, verb);

            return verb;
        }
    }
}