using Lab1sharp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices.Marshalling;
using System.Text;

namespace Lab1sharp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DecodeController : ControllerBase
    {
        public IActionResult Decode([FromQuery] string text)
        {
            string decodedText = Util.Decode(text);
            var responseObject = new 
            {
                Operation = "Decoder",
                Author = "Team A",
                DecodedCiphertext = decodedText
            };
            return new JsonResult(responseObject);
        }
    }
}
