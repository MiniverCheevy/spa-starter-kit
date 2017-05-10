//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Fernweh.Core;
//using Fernweh.Web.Controllers.Api;
//using Voodoo.Messages;
//using System.IO;
//using Microsoft.AspNetCore.Hosting;
//using System.Drawing;
//using Fernweh.Core.Helpers;
//using Voodoo.Logging;

//namespace Fernweh.Web.Controllers.Files
//{
//    [CustomAuthorize(Roles = RoleNames.Administrator)]
//    public class ClientLogoUpload : Controller
//    {
//        private IHostingEnvironment environment;

//        public ClientLogoUpload (IHostingEnvironment environment)
//        {
//            this.environment = environment;
//        }
//        [HttpPost]
//        public async Task<JsonResult> Index(int id)
//        {
//            var response = new TextResponse();

//            try
//            {
//                var request = new ImageRequest
//                {
//                    FileName = System.Net.WebUtility.UrlDecode(Request.Headers["X-File-Name"]),
//                    RootPath = environment.WebRootPath,
//                    Stream = Request.Body
//                };
//                var image = ImageHelper.WriteImage(request);
//                response.Text = image;
//                return Json(response);
//            }

//            catch (Exception ex)
//            {
//                LogManager.Log(ex);
//                response.SetExceptions(ex);
//                return Json(response);
//            }
//        }
//    }
//}


