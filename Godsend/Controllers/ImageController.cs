// <copyright file="ImageController.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Godsend.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Image controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Route("api/[controller]")]
    public class ImageController : Controller
    {
        /// <summary>
        /// The repository
        /// </summary>
        private ImageRepository repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageController"/> class.
        /// </summary>
        /// <param name="repo">The repo.</param>
        public ImageController(ImageRepository repo)
        {
            repository = repo;
        }

        /// <summary>
        /// Gets the preview image.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Base64-encoded image</returns>
        [HttpGet("[action]/{id:Guid}")]
        public JsonResult GetPreviewImage(Guid id)
        {
            try
            {
                return Json(Convert.ToBase64String(System.IO.File.ReadAllBytes("Images/" + repository.GetImage(id))));
            }
            catch
            {
                // no image for id (new entities)
                return null;
            }

            ////return File(new FileStream("Images/"+repository.GetImage(id), FileMode.Open, FileAccess.Read), "image/jpeg");
        }

        /// <summary>
        /// Gets the images.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Array of base64-encoded images</returns>
        [HttpGet("[action]/{id:Guid}")]
        public IEnumerable<string> GetImages(Guid id)
        {
            var images = new List<string>();
            foreach (string fileName in repository.GetImages(id))
            {
                images.Add(Convert.ToBase64String(System.IO.File.ReadAllBytes("Images/" + fileName)));
            }

            return images;
        }

        /// <summary>
        /// Gets the preview images.
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <returns>dictionary of (id, base64-encoded image) pairs</returns>
        [HttpPost("[action]")]
        public IDictionary<Guid, string> GetPreviewImages([FromBody]Guid[] ids)
        {
            var res = new Dictionary<Guid, string>();
            foreach (Guid id in ids)
            {
                var tmp = GetPreviewImage(id);
                if (tmp != null)
                {
                    res.Add(id, GetPreviewImage(id).Value.ToString());
                }
            }

            return res;
        }

        /// <summary>
        /// Uploads the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Ok</returns>
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
