using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace CXMLAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CXMLController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnv;

        public CXMLController(IWebHostEnvironment webHostEnv)
        {
            _webHostEnv = webHostEnv;
        }

        [HttpPost]
        public IActionResult PostCXMLFile([FromForm] IFormFile file)
        {
            string dir = "/Uploads/XML";
            DateTime now = DateTime.Now;
            string uploadDirectory = $"{dir}/{now.Year}/{now.Month}/{now.Day}/{now.Hour}";
            string path = _webHostEnv.ContentRootPath + uploadDirectory;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string fileName = Guid.NewGuid().ToString().Replace("-", "") + file.FileName;
            using (FileStream stream = new(Path.Combine(path, fileName), FileMode.Create))
            {
                file.CopyTo(stream);
            }
            string filePath = Path.Combine(path, fileName);
            return Ok();
        }
    }
}
