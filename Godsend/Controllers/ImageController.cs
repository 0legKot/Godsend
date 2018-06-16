using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Godsend.Models;

namespace Godsend.Controllers
{
    [Route("api/[controller]")]
    public class ImageController : Controller
    {
        ImageRepository repository;
        public ImageController(ImageRepository repo)
        {
            repository = repo;
        }

        [HttpGet("[action]/{id:Guid}")]
        public JsonResult GetPreviewImage(Guid id)
        {
            try
            {
                return Json(Convert.ToBase64String(System.IO.File.ReadAllBytes("Images/" + repository.GetImage(id))));

            }
            catch // no image for id (new entities)
            {
                return null;
            }
            //return File(new FileStream("Images/"+repository.GetImage(id), FileMode.Open, FileAccess.Read), "image/jpeg");
        }

        [HttpGet("[action]/{id:Guid}")]
        public IEnumerable<string> GetImages(Guid id)
        {
            var images = new List<string>();
            foreach (string fileName in repository.GetImages(id))
            images.Add(Convert.ToBase64String(System.IO.File.ReadAllBytes("Images/"+fileName)));
            return images;
        }


        [HttpPost("[action]")]
        public IDictionary<Guid, string> GetPreviewImages([FromBody]Guid[] ids)
        {
            var res = new Dictionary<Guid, string>();
            foreach (Guid id in ids)
                //check tostring if smth doesnt working
            {
                var tmp = GetPreviewImage(id);
                if (tmp != null) res.Add(id, GetPreviewImage(id).Value.ToString());
            }
            return res;

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
