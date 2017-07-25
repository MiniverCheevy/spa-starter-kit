//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Net.Mime;
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
//    public class DailyWorkReportFileDownload : Controller
//    {
//        private IHostingEnvironment environment;

//        public DailyWorkReportFileDownload(IHostingEnvironment environment)
//        {
//            this.environment = environment;
//        }

//        [HttpGet]
//        public async Task<FileResult> Get([FromQuery]int id)
//        {
//            BinaryResponse response = new BinaryResponse();
//            try
//            {

//                response = await new DailyWorkReportFileDetailQuery(new IdRequest { Id = id }).ExecuteAsync();
//                if (response.IsOk)
//                {

//                    var inline = response.ContentDisposition == "inline";
//                    var cd = new ContentDisposition
//                    {
                        
//                        Inline = inline
//                    };
//                    if (!inline)
//                        cd.FileName = response.FileName;

//                    Response.Headers.Add("Content-Disposition", cd.ToString());
//                    Response.Headers.Add("X-Content-Type-Options", "nosniff");

                    
//                        return File(response.Data, response.ContentType);
                    
//                }
//                return null;
//            }
//            catch (Exception ex)
//            {
//                response.SetExceptions(ex);
//                return null;
//            }
//        }
//    }
//}

