﻿using Core;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Api
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