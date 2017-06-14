//using System;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Fernweh.Core.Operations.InspectionPdf;
//using Fernweh.Core.Operations.Inspections;
//using Fernweh.Web.Controllers.Api;
//using Fernweh.Web.Wkhtml;
//using Voodoo.Logging;

//namespace Fernweh.Web.Controllers
//{
//    [CustomAuthorize]
//    public class PrintController : Controller
//    {
//        private IPdfPrinter printer;

//        public PrintController(IPdfPrinter printer)
//        {
//            this.printer = printer;
//        }

//        public async Task<FileResult> Print(int id)
//        {
//            try
//            {
//                var op = new  InspectionPdfQuery  (new InspectionQueryRequest { InspectionResultId = id });
//                var response = await op.ExecuteAsync();
//                //var html = String.Concat(response.Data.Header, response.Data.Lookup, response.Data.Markup);
//                return File(response.Data, "application/pdf", "inspection.pdf");
//            }
//            catch (Exception ex)
//            {

//                LogManager.Log(ex);
//            }

//            return null;            
//        }
//    }
//}

