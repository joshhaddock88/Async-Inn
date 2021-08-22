using Microsoft.EntityFrameworkCore;
using AsyncInn.Data;
using AsyncInn.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AsyncInn.Models.DTOs;

namespace AsyncInn.Models.Services
{
    public class HotelService : IHotel
    {
        private AsyncInnDbContext _context;

        public HotelService(AsyncInnDbContext context)
        {
            _context = context;
        }

        public async Task AddRoom(int roomId, int hotelId)
        {
            HotelRoom hotelRoom = new HotelRoom()
            {
                RoomId = roomId,
                HotelId = hotelId
            };

            _context.Entry(hotelRoom).State = EntityState.Added;
            await _context.SaveChangesAsync();
        }

        public async Task<Hotel> Create(Hotel hotel)
        {
            _context.Entry(hotel).State = EntityState.Added;

            await _context.SaveChangesAsync();
            return hotel;
        }

        public async Task Delete(int id)
        {
            Hotel hotel = await _context.Hotels.FindAsync();
            _context.Entry(hotel).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<HotelDTO> GetHotel(int id)
        {
            return await _context.Hotels
                .Select(hotel => new HotelDTO
                {
                    Id = hotel.Id,
                    Name = hotel.Name,
                    City = hotel.City,
                    Rooms = hotel.HotelRooms
                    .Select(h => new HotelRoomDTO
                    {
                        Room = h.Room.Name
                    }).ToList()
                }).FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Hotel> GetHotelByName(string name)
        {
            return await _context.Hotels
                .Include(a => a.HotelRooms)
                .ThenInclude(b => b.Room)
                .FirstOrDefaultAsync(m => m.Name == name);
        }

        public async Task<List<HotelDTO>> GetHotels()
        {
            return await _context.Hotels
                .Select(hotel => new HotelDTO
                {
                    Id = hotel.Id,
                    Name = hotel.Name,
                    City = hotel.City,
                    Rooms = hotel.HotelRooms
                    .Select(h => new HotelRoomDTO
                    {
                        Room = h.Room.Name
                    }).ToList()
                }).ToListAsync();
        }

        public async Task<Hotel> UpdateHotel(int id, Hotel hotel)
        {
            _context.Entry(hotel).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return hotel;
        }
    }
}
