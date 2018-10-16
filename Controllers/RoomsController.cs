using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureASPNetCoreAPIs.Context;
using SecureASPNetCoreAPIs.Models;

namespace SecureASPNetCoreAPIs.Controllers {

    [Route ("/[controller]")]
    public class RoomsController : Controller {
        private readonly HotelApiContext _context;

        public RoomsController (HotelApiContext context) {
            _context = context;
        }

        [HttpGet (Name = nameof (GetRooms))]
        public IActionResult GetRooms () {
            throw new NotImplementedException ();
        }

        // room/{roomId}

        [HttpGet ("{roomId}", Name = nameof (GetRoomByIdAsync))]
        public async Task<IActionResult> GetRoomByIdAsync (int roomId, CancellationToken ct) {
            var entity = await _context.Rooms.SingleOrDefaultAsync (r => r.Id == roomId);
            if (entity == null) return NotFound ();

            var resource = new Room {
                Href = Url.Link (nameof (GetRoomByIdAsync), new { roomId = entity.Id }),
                Name = entity.Name,
                Rate = entity.Rate / 100.0
            };

            return Ok (resource);
        }
    }
}