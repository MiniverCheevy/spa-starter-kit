using Fernweh.Core;
using Fernweh.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Fernweh.Aurelia.Controllers.Api
{
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        [HttpGet]
        public string Get()
        {
            var test = IOC.RequestContext.AppPrincipal;
            return "string";
        }
    }
}