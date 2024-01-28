using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Lab1sharp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DecodeController : ControllerBase
    {
        public IActionResult Decode([FromQuery] string text)
        {
            var responseObject = new 
            {
                Operation = "Decoder",
                Author = "Team A",
                DecodedCiphertext = "The decoded result from your text: " + text
            };
            return new JsonResult(responseObject);
        }
    }
}
