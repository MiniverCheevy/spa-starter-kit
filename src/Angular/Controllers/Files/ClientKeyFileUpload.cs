//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Fernweh.Core;
//using Fernweh.Web.Controllers.Api;
//using Voodoo.Messages;

//namespace Fernweh.Web.Controllers.Files
//{
//    [CustomAuthorize(Roles = RoleNames.QualityControlAdmin)]
//    public class ClientKeyFileUpload : Controller
//    {
//        private IHostingEnvironment environment;

//        public ClientKeyFileUpload(IHostingEnvironment environment)
//        {
//            this.environment = environment;
//        }

//        [HttpPost]
//        public async Task<JsonResult> Index(int id)
//        {
//            var response = new Response();
//            try
//            {

//                using (var memoryStream = new MemoryStream())
//                {

//                    var type = Request.Headers["X-File-Type"].ToString();
//                    var fileName = System.Net.WebUtility.UrlDecode(Request.Headers["X-File-Name"]);
//                    if (type.IndexOf("text/csv", StringComparison.Ordinal) > -1 ||
//                        type.IndexOf("excel", StringComparison.Ordinal) > -1 ||
//                        type.IndexOf("openxmlformats", StringComparison.Ordinal) > -1 ||
//                        type.IndexOf("officedocument", StringComparison.Ordinal) > -1)
//                    {
//                        await Request.Body.CopyToAsync(memoryStream);
//                        var request = new KeyImportRequest()
//                        {
//                            Data = memoryStream.ToArray(),
//                            Level = id
//                        };
//                        response = await new KeyImportCommand(request).ExecuteAsync();
//                    }
//                    else
//                    {
//                        response.IsOk = false;
//                        response.Message = "Only csv files are allowed.";
//                    }
//                    return Json(response);
//                }

//            }
//            catch (Exception ex)
//            {
//                response.SetExceptions(ex);
//                return Json(response);
//            }
//        }
//    }
//}

