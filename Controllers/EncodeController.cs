using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace Lab1sharp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EncodeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Encode([FromQuery] string text)
        {
            var responseObject = new 
            {
                Operation = "Encoder",
                Author = "Team A",
                EncodedCiphertext = "pretent encode the text you entered: " + text
            };
            return new JsonResult(responseObject);
        }
    }
}
