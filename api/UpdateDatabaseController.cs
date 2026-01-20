using api;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Route("api/update")]
    [ApiController]
    public class UpdateDatabaseController : ControllerBase
    {
        [HttpPost]
        [Produces("application/json")]
        public IActionResult Update (IFormFile file)
        {
            new UpdateHandler().UploadCSV(file);
            return Ok();
        }
    }
}
