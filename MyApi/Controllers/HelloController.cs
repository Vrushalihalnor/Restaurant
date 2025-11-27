using Microsoft.AspNetCore.Mvc;

namespace MyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HelloController : ControllerBase
    {
        
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hello! Your API is running successfully.");
        }
        [HttpGet("{name}")]
        public IActionResult GetByName(string name)
        {
            return Ok($"Hello, {name}! Welcome to the API.");
        }
    }
}
