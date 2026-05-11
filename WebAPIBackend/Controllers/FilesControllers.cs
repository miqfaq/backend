using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPIBackend.Models.DTO;
using WebAPIBackend.Models.Employees;

namespace WebAPIBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class FilesController: ControllerBase
    {
        const string FILES_DIRECTORY = "filesStorage";
        [HttpPost("upload")]
        [Authorize(Roles = "admin, manager")]
        public IActionResult UploadFile([FromBody] FileRequestDto filedto)
        {
            try
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), FILES_DIRECTORY);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                var fileBytes = Convert.FromBase64String(filedto.FileString);
                System.IO.File.WriteAllBytes(Path.Combine(path, filedto.FileName), fileBytes);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("download")]
        public IActionResult DownloadFile(string filename)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), FILES_DIRECTORY);
            if (!Directory.Exists(path))
            {
                return BadRequest("File not found");
            }
            var fileBytes = System.IO.File.ReadAllBytes(Path.Combine(path, filename));
            return File(fileBytes, "application/octet-stream", filename);
        }
        [HttpDelete("delete")]
        [Authorize(Roles = "admin, manager")]
        public IActionResult DeleteFile(string fileName)
        {
            try
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), FILES_DIRECTORY);
                if (!Directory.Exists(path))
                {
                    return BadRequest("File not found");
                }
                System.IO.File.Delete(Path.Combine(path, fileName));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}