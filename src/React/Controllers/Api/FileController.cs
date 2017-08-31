using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Voodoo.Messages;

namespace Web.Controllers.Api
{
    public class FileController : Controller
    {
        protected ActionResult HandleBinaryResponse(BinaryResponse response)
        {
            if (response.IsOk)
            {
                var inline = response.ContentDisposition == "inline";
                var cd = new ContentDisposition
                {
                    Inline = inline
                };
                if (!inline)
                    cd.FileName = response.FileName;

                Response.Headers.Add("Content-Disposition", cd.ToString());
                Response.Headers.Add("X-Content-Type-Options", "nosniff");

                return File(response.Data, response.ContentType);
            }
            return Content(response.Message);
        }
    }
}

