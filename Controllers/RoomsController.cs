using System;
using Microsoft.AspNetCore.Mvc;

namespace SecureASPNetCoreAPIs.Controllers
{
    [Route("/[controller]")]
    public class RoomsController : Controller
    {
        [HttpGet(Name = nameof(GetRooms))]
        public IActionResult GetRooms ()
        {
            throw new NotImplementedException();
        }
    }
}