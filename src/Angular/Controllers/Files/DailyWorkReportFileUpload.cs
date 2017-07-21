//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Threading.Tasks;
//using Core.Operations.DailyWorkReportFiles;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Mvc;
//using Voodoo.Messages;
//using Core.Operations.DailyWorkReportFiles.Extras;
//using Microsoft.AspNetCore.Http;

//namespace Web.Controllers.Files
//{
//    [Route("api/[controller]")]
//    public class DailyWorkReportFileUpload : Controller
//    {
//        private IHostingEnvironment environment;

//        public DailyWorkReportFileUpload(IHostingEnvironment environment)
//        {
//            this.environment = environment;
//        }

//        [HttpPost]        
//        public async Task<JsonResult> Post([FromQuery]int id)
//        {
//            var response = new Response();
//            try
//            {

//                using (var memoryStream = new MemoryStream())
//                {
//                    await Request.Body.CopyToAsync(memoryStream);
//                    var request = new DailyWorkReportFileDetail
//                    {
//                        Data = memoryStream.ToArray(),
//                        FileName = System.Net.WebUtility.UrlDecode(Request.Headers["X-File-Name"]),
//                        MimeType = Request.Headers["X-File-Type"],
//                        DailyWorkReportId = id
//                    };
//                    response = await new AddDailyWorkReportFileCommand(request).ExecuteAsync();

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

