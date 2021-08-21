using Microsoft.EntityFrameworkCore;
using AsyncInn.Data;
using AsyncInn.Models.Interfaces;
using AsyncInn.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models.Services
{
    public class RoomService : IRoom
    {

        private AsyncInnDbContext _context;

        public RoomService(AsyncInnDbContext context)
        {
            _context = context;
        }

        public async Task<Room> Create(Room room)
        {
            // student is an instance of Room
            // the current state of the room object: raw

            _context.Entry(room).State = EntityState.Added;
            // the current state of th estudent object: added

            await _context.SaveChangesAsync();

            return room;
        }

        public async Task<List<Room>> GetRooms()
        {
            return await _context.Rooms
                .Include(c => c.RoomAmenities)
                .ThenInclude(e => e.Amenity)
                .ToListAsync();
        }

        public async Task<Room> GetRoom(int id)
        {
            return await _context.Rooms
                .Include(c => c.RoomAmenities)
                .ThenInclude(e => e.Amenity)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Room> UpdateRoom(int id, Room room)
        {
            _context.Entry(room).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return room;
        }

        public async Task Delete(int id)
        {
            Room room = await GetRoom(id);
            _context.Entry(room).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task AddAmenity(int roomId, int amenityId)
        {
            RoomAmenities RoomAmenity = new RoomAmenities()
            {
                AmenityId = amenityId,
                RoomId = roomId
            };
            _context.Entry(RoomAmenity).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }

        public Task RemoveAmenity(int roomId, int amenityId)
        {
            throw new NotImplementedException();
        }

        /*        public async Task RemoveAmenity(int roomId, int amenityId)
                {           
                }*/
    }
}
