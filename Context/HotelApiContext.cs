using Microsoft.EntityFrameworkCore;
using SecureASPNetCoreAPIs.Models;

namespace SecureASPNetCoreAPIs.Context {
    public class HotelApiContext : DbContext {
        public HotelApiContext (DbContextOptions options) : base (options) { }

        public DbSet<RoomEntity> Rooms { get; set; }
    }
}