//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Fernweh.Core.Operations.InspectionResults;
//using Fernweh.Core.Operations.InspectionResults.Extras;
//using Fernweh.Web.Controllers.Api;

//namespace Fernweh.Web.Controllers
//{
//    [CustomAuthorize]
//    public class InspectionResultValueController : Controller
//    {
//		[HttpPost]
//        public async Task<JsonResult> Put
//       (InspectionResultRequest request)
//        {
//            var op = new InspectionResultUpdateCommand(request);
//            var response = await op.ExecuteAsync();
//            return Json(response);
//        }

//    }
//}

