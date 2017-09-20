using System;
using Core.Identity;

namespace Core.Infrastructure
{
    public class RequestContext
    {
        public AppPrincipal AppPrincipal { get; set; }
        public ClientInfo ClientInfo { get; set; }
       
    }
}