using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIBackend.DbContexts;
using WebAPIBackend.Models.DTO;
using WebAPIBackend.Models.Users;

namespace WebAPIBackend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class FilesController: ControllerBase
    {
        const string FILES_DIRECTORY = "filesStorage";
        private AplicationContext _context;


        public FilesController()
        {
            _context = new AplicationContext();
        }


        [HttpPost("upload")]
        [Authorize(Roles = "admin, manager")]
        public IActionResult UploadFile([FromBody] FileRequestDto filedto)
        {
            try
            {
                var user = _context.Users.Include(e=>e.UserFiles).FirstOrDefault(e => e.Id == filedto.UserId);

                if (user == null)
                {
                    return BadRequest("User not found");
                }
                

                var path = Path.Combine(Directory.GetCurrentDirectory(), FILES_DIRECTORY);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                var fileBytes = Convert.FromBase64String(filedto.FileString);
                var systemFileName = Guid.NewGuid().ToString();
                System.IO.File.WriteAllBytes(Path.Combine(path, filedto.FileName), fileBytes);

                if (user.UserFiles == null)
                {
                    user.UserFiles = new List<UserFile>();
                }
                user.UserFiles.Add(new UserFile
                {
                    DisplayName = filedto.FileName,
                    SystemName = systemFileName
                });
                _context.SaveChanges();

                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("download")]
        public IActionResult DownloadFile([FromBody] DownloadFileRequestDto downloadFileRequestDto)
        {
            try
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), FILES_DIRECTORY);
                if (!Directory.Exists(path))
                {
                    return BadRequest("File not found");
                }
                var filebytes = System.IO.File.ReadAllBytes(Path.Combine(path, downloadFileRequestDto.SystemName));
                return File(filebytes, "application/octet-stream", downloadFileRequestDto.DisplayName);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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