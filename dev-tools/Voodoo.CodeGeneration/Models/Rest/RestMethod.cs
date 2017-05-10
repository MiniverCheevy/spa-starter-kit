using System;
using System.Linq;
using Voodoo.Infrastructure;

namespace Voodoo.CodeGeneration.Models.Rest
{
    [Serializable]
    public class RestMethod : Operation
    {
        public Verb Method { get; set; }

        public string Attribute { get; set; }
        public string Name { get; set; }
        public string Parameter { get; set; }
        public bool AllowAnonymous { get; set; }
        public string[] Roles { get; set; } = { };

        public string RoleString => !Roles.Any()
            ? null
            : string.Join(",", Roles).TrimEnd(',');

        public string RoleArrayString => string.Join(",", Roles.Select(c => $"\"{c}\"").ToArray());

        public RestMethod()
        {
            Roles = new string[] { };
        }
    }
}