using Microsoft.AspNetCore.Mvc;
using SecureASPNetCoreAPIs.Models;

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
                rooms = Url.Link(nameof(RoomsController.GetRooms), null),
                info = Url.Link(nameof(InfoController.GetInfo), null)
            };

            return Ok(response);
        }
    }
}
