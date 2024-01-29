using Lab1sharp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace Lab1sharp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EncodeController : ControllerBase
    {
        Logger logger = new Logger();
        [HttpGet]
        public IActionResult Encode([FromQuery] string text)
        {
            string encodedText = Util.Encode(text);
            var responseObject = new 
            {
                Operation = "Encoder",
                Author = "Team A",
                EncodedCiphertext = encodedText
            };
            logger.Log(text, "encode");
            return new JsonResult(responseObject);
        }
    }
}
