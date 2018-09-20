using Microsoft.AspNetCore.Mvc;

namespace SecureASPNetCoreAPIs.Controllers
{
    [Route("/")]
    [ApiVersion("1.0")]
    public class RootController : Controller
    {
        [HttpGet(Name = nameof(GetRoot))]
        public IActionResult GetRoot () {
            var response =  new {
                href = Url.Link(nameof(GetRoot), null),
                rooms = Url.Link(nameof(RoomsController.GetRooms), null)
            };

            return Ok(response);
        }
    }
}