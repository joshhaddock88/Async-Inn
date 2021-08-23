using AsyncInn.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Data
{
    public class AsyncInnDbContext : IdentityDbContext<ApplicationUser>
    {
        public AsyncInnDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Hotel> Hotels { get; set; }        
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<HotelRoom> HotelRooms { get; set; }
        public DbSet<RoomAmenity> RoomAmenities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // This calls the base method, but does nothing
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Hotel>().HasData(
              new Hotel { Id = 1, Name = "Async Emerald", StreetAddress = "116 1st Ave", State = "WA", City = "Seattle", Country = "USA", Phone = "(555)555-5555" },
              new Hotel { Id = 2, Name = "Async PDX", StreetAddress = "12 2nd Blvd", State = "OR", City = "Portland", Country = "USA", Phone = "(515)515-5115" },
              new Hotel { Id = 3, Name = "Async SeaSide", StreetAddress = "42 Sunny Sundown St.", State = "CA", City = "Los Angeles", Country = "USA", Phone = "(525)525-5225" }
            );

            modelBuilder.Entity<Room>().HasData(
                new Room { Id = 1, Name = "The Deuces", Layout = 2 },
                new Room { Id = 2, Name = "Life of Pablo", Layout = 0 },
                new Room { Id = 3, Name = "Two Plus", Layout = 2 }
            );

            modelBuilder.Entity<Amenity>().HasData(
                new Amenity { Id = 1, Name = "Toaster"},
                new Amenity { Id = 2, Name = "Ocean View" },
                new Amenity { Id = 3, Name = "Pommel Horse" }
            );

            SeedRole(modelBuilder, "Regional manager", "createRoom", "createAmenity", "createHotel",
                "updateRoom", "updateAmenity", "updateHotel", "deleteRoom", "deleteAmenity", "deleteHotel");

            SeedRole(modelBuilder, "Property Manager", "createRoom", "createAmenity", "updateRoom", 
                "updateAmenity", "deleteAmenity");

            SeedRole(modelBuilder, "Agent", "createAmenity", "updateRoom", "updateAmenity", 
                "deleteAmenity");

            modelBuilder.Entity<HotelRoom>().HasKey(
                hotelRoom => new {hotelRoom.RoomId, hotelRoom.HotelId}
            );

            modelBuilder.Entity<RoomAmenity>().HasKey(
                    roomAmenity => new { roomAmenity.RoomId, roomAmenity.AmenityId}
                );
        }

        private int nextId = 1;
        public void SeedRole(ModelBuilder modelBuilder, string roleName, params string[] permission)
        {
            var role = new IdentityRole
            {
                Id = roleName.ToLower(),
                Name = roleName,
                NormalizedName = roleName.ToUpper(),
                ConcurrencyStamp = Guid.Empty.ToString()
            };

            modelBuilder.Entity<IdentityRole>().HasData(role);

            var roleClaims = permission.Select(permission =>
                new IdentityRoleClaim<string>
                {
                    Id = nextId++,
                    RoleId = role.Id,
                    ClaimType = "permissions",
                    ClaimValue = permission
                }
            ).ToArray();

            modelBuilder.Entity<IdentityRoleClaim<string>>().HasData(roleClaims);
        }
    }
}
