using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class BrokenController : Controller
    {

        [HttpGet]
        public ActionResult Get()
        {
            throw new Exception("Jenkies");
        }

    }
}
