using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using net_core_based.Models;

namespace net_core_based.Controllers
{
    [Route("api/upload")]
    [ApiController]
    [Authorize]
    public class UploadController : ControllerBase
    {
        [HttpPost("file")]
        public async Task<IActionResult> UploadFile([FromForm] UploadModel model)
        {
            try
            {
                var file = model.File;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", file.FileName);
                using var stream = System.IO.File.Create(path);
                await file.CopyToAsync(stream);
                return new ObjectResult(new
                {
                    Status = "Success",
                    Path = path,

                })
                {
                    StatusCode = StatusCodes.Status201Created,
                };
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
