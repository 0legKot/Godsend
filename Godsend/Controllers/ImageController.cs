using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Godsend.Controllers
{
    [Route("api/[controller]")]
    public class ImageController : Controller
    {

        [HttpGet("[action]/{id:Guid}")]
        public FileResult GetImage(Guid id)
        {
            return File(new FileStream("Images/apple.jpg", FileMode.Open, FileAccess.Read), "image/jpeg");
        }

        [HttpGet("[action]/{id:Guid}")]
        public string[] GetImages(Guid id)
        {
            var a = Convert.ToBase64String(System.IO.File.ReadAllBytes("Images/apple.jpg"));
            return new[] { a, a };
        }


        [HttpGet("[action]")]
        public string[] GetPreviewImages([FromBody]Guid[] ids)
        {
            throw new NotImplementedException();
        }

        [HttpPost("[action]/{id:Guid}")]
        public IActionResult Upload(Guid id)
        {
            var files = Request.Form.Files;

            // validate
            // save files and save paths to db

            return Ok();
        }

        // Delete
    }
}
