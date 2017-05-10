//using System;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Mvc;
//using Fernweh.Core.Operations.InspectionFiles;
//using Fernweh.Web.Controllers.Api;
//using Voodoo.Logging;
//using Voodoo.Messages;

//namespace Fernweh.Web.Controllers.Files
//{
//    [CustomAuthorize]
//    public class InspectionFileDownload : Controller
//    {
//        private IHostingEnvironment environment;

//        public InspectionFileDownload(IHostingEnvironment environment)
//        {
//            this.environment = environment;
//        }
//        public async Task<FileResult> Index(int id)
//        {

//            try
//            {
//                var response = await new InspectionFileDetailQuery(new IdRequest { Id = id }).ExecuteAsync();
//                if (response.IsOk)
//                {
//                    return File(response.Data.Data, response.Data.MimeType, response.Data.FileName);
//                }
//            }
//            catch (Exception ex)
//            {
//                LogManager.Log(ex);

//            }
//            var file = $"{this.environment.ContentRootPath}\\wwwroot\\assets\\error.png";
//            var bytes = System.IO.File.ReadAllBytes(file);
//            return File(bytes, "image/png");
//        }

//    }
//}


