//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Fernweh.Core.Operations.InspectionFiles;
//using Fernweh.Core.Operations.InspectionFiles.Extras;
//using Voodoo.Messages;

//namespace Fernweh.Web.Controllers.Files
//{
//    public class InspectionFileUpload : Controller
//    {
//        private IHostingEnvironment environment;

//        public InspectionFileUpload(IHostingEnvironment environment)
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
//                    await Request.Body.CopyToAsync(memoryStream);
//                    var request = new InspectionFileDetail
//                    {
//                        Data = memoryStream.ToArray(),
//                        FileName = System.Net.WebUtility.UrlDecode(Request.Headers["X-File-Name"]),
//                        MimeType = Request.Headers["X-File-Type"],
//                        InspectionResultId = id
//                    };
//                        response = await new InspectionFileAddCommand(request).ExecuteAsync();

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


