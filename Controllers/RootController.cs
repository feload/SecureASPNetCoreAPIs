using Microsoft.AspNetCore.Mvc;

namespace SecureASPNetCoreAPIs.Controllers
{
    [Route("/")]
    [ApiVersion("1.0")]
    //[ApiController]
    public class RootController : Controller
    {
        [HttpGet(Name = nameof(GetRoot))]
        public IActionResult GetRoot () {
            var response =  new {
                href = Url.Link(nameof(GetRoot), null)
            };

            return Ok(response);
        }
    }
}