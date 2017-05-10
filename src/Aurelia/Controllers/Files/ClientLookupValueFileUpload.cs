//using System;
//using System.IO;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Mvc;
//using Fernweh.Core;
//using Fernweh.Web.Controllers.Api;
//using Voodoo.Messages;
//using System.Linq;
//using Voodoo.Logging;

//namespace Fernweh.Web.Controllers.Files
//{
//    [CustomAuthorize(Roles = RoleNames.QualityControlAdmin)]
//    public class ClientLookupValueFileUpload : Controller
//    {
//        private IHostingEnvironment environment;

//        public ClientLookupValueFileUpload(IHostingEnvironment environment)
//        {
//            this.environment = environment;
//        }

//        [HttpGet]
//        public ActionResult GetUploadTemplate()
//        {
//            try
//            {
//                string header = LookupValueImportHeaderValidator.GenerateLookupImportCsvHeader();
//                var bytes = System.Text.Encoding.UTF8.GetBytes(header);

//                return File(bytes, "text/csv", "LookupValueImportTemplate.csv");
//            }
//            catch(Exception e)
//            {
//                LogManager.Log(e);
//                var response = new Response();
//                response.HasLogicException = true;
//                response.IsOk = false;
//                response.Message = e.Message;
//                return Json(response);
//            }
//        }


//        [HttpPost]
//        public async Task<JsonResult> Index()
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
//                        var request = new LookupValueImportRequest() 
//                        {
//                            Data = memoryStream.ToArray()
//                        };
//                        response = await new LookupValueImportCommand(request).ExecuteAsync();
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


