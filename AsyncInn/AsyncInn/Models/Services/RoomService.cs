using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AsyncInn.Data;
using AsyncInn.Models.DTOs;
using AsyncInn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AsyncInn.Models.Services
{
    public class RoomService : IRoom
    {

        private AsyncInnDbContext _context;
        private IHotel _hotels;

        public RoomService(AsyncInnDbContext context, IHotel hotelService, IAmenity amenityService)
        {
            _context = context;
            _hotels = hotelService;
        }

        public async Task AddAmenity(int roomId, int amenityId)
        {
            RoomAmenity RoomAmenity = new RoomAmenity()
            {
                AmenityId = amenityId,
                RoomId = roomId
            };

            _context.Entry(RoomAmenity).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }

        public async Task<RoomDTO> Create(NewRoomDTO paramRoom)
        {
            Room room = new Room()
            {
                Name = paramRoom.Name,
                Layout = paramRoom.Layout
            };

            _context.Entry(room).State = EntityState.Added;
            await _context.SaveChangesAsync();

            Hotel hotel = await _hotels.GetHotelByName(paramRoom.HotelName);

            await _hotels.AddRoom(room.Id, hotel.Id);

            RoomDTO addedRoom = await GetRoom(room.Id);

            return addedRoom;
        }

        public async Task Delete(int id)
        {
            Room room = await _context.Rooms.FindAsync(id);
            _context.Entry(room).State = EntityState.Deleted;

            await _context.SaveChangesAsync();
        }

        public async Task<RoomDTO> GetRoom(int id)
        {
            return await _context.Rooms
                .Select(room => new RoomDTO
                {
                    Id = room.Id,
                    Name = room.Name,
                    Layout = room.Layout,
                    Amenities = room.RoomAmenities
                    .Select(t => new AmenityDTO
                    {
                        Name = t.Name
                    }).ToList()
                }).FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<List<RoomDTO>> GetRooms()
        {
            return await _context.Rooms
                .Select(room => new RoomDTO
                {
                    Id = room.Id,
                    Name = room.Name,
                    Layout = room.Layout,
                    Amenities = room.RoomAmenities
                    .Select(t => new AmenityDTO
                    {
                        Name = t.Name
                    }).ToList()
                }).ToListAsync();
        }

        public async Task RemoveAmenity(int roomId, int amenityId)
        {
            Room room = await _context.Rooms.FindAsync(roomId);
            List<Amenity> ra = room.RoomAmenities;
            for (int i = 0; i < ra.Count; i++)
            {
                if(ra[i].Id == amenityId)
                {
                    _context.Entry(ra[i].State = EntityState.Deleted);
                    await _context.SaveChangesAsync();
                    break;
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task<Room> UpdateRoom(int id, Room room)
        {
            _context.Entry(room).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return room;
        }
    }
}
