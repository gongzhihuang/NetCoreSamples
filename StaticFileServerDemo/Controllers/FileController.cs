using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StaticFileServerDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileController : ControllerBase
    {
        private readonly ILogger<FileController> _logger;

        public FileController(ILogger<FileController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="file">文件</param>
        /// <returns>文件名</returns>
        [HttpPost("uploadfile")]
        public IActionResult UploadFile(IFormFile file)
        {
            try
            {
                string[] fileStr = file.FileName.Split(".");
                if (file != null && fileStr.Length >= 2)
                {
                    string guid = Guid.NewGuid().ToString();

                    string fileName = guid + "." + fileStr[fileStr.Length - 1];
                    var files = Path.Combine(Directory.GetCurrentDirectory(), "files", fileName);

                    using (FileStream fs = System.IO.File.Create(files))
                    {
                        file.CopyTo(fs);
                        fs.Flush();
                    }

                    return Ok(fileName);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception)
            {
                return NoContent();
            }
        }

        /// <summary>
        /// 文件下载
        /// </summary>
        /// <param name="fileName">文件名称(包含后缀, 如:333.xml)</param>
        /// <returns></returns>
        [HttpGet("downloadfile")]
        public IActionResult DownloadFile(string fileName)
        {
            if (!string.IsNullOrWhiteSpace(fileName))
            {
                var file = Path.Combine(Directory.GetCurrentDirectory(), "files", fileName);

                string fileExt = Path.GetExtension(fileName);
                var provider = new FileExtensionContentTypeProvider();
                var memi = provider.Mappings[fileExt];

                return PhysicalFile(file, memi);
            }
            return NotFound();
        }
    }
}
