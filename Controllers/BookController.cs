
using Microsoft.AspNetCore.Mvc;

namespace LibraNet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Test");
        }
    }
}