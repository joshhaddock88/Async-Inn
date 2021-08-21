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

        public async Task<HotelDTO> Create(Hotel hotel)
        {
            // hotel is an instance of Hotel
            // the current state of the hotel object: raw

            _context.Entry(hotel).State = EntityState.Added;
            // the current state of the student object: added
            
            await _context.SaveChangesAsync();

            return await _context.Hotels
                .Select(hotel => new HotelDTO()
                {
                    Id = hotel.Id,
                    Name = hotel.Name,
                    StreetAddress = hotel.StreetAddress,
                    City = hotel.City,
                    State = hotel.State,
                    Phone = hotel.Phone,
                    Country = hotel.Country
                }).FirstOrDefaultAsync(h => h.Id == hotel.Id);
        }

        public async Task<List<HotelDTO>> GetHotels()
        {
            var hotels = await _context.Hotels.ToListAsync();
            return await _context.Hotels
                .Select(hotel => new HotelDTO()
                {
                    Id = hotel.Id,
                    Name = hotel.Name,
                    StreetAddress = hotel.StreetAddress,
                    City = hotel.City,
                    State = hotel.State,
                    Phone = hotel.Phone,
                    Country = hotel.Country
                }).ToListAsync();
        }

        public async Task<HotelDTO> GetHotel(int id)
        {
            Hotel hotel = await _context.Hotels.FindAsync(id);
            return await _context.Hotels
                .Select(hotel => new HotelDTO()
                {
                    Id = hotel.Id,
                    Name = hotel.Name,
                    StreetAddress = hotel.StreetAddress,
                    City = hotel.City,
                    State = hotel.State,
                    Phone = hotel.Phone,
                    Country = hotel.Country
                }).FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task<HotelDTO> UpdateHotel(int id, Hotel hotel)
        {
            _context.Entry(hotel).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return await _context.Hotels
                .Select(hotel => new HotelDTO()
                {
                    Id = hotel.Id,
                    Name = hotel.Name,
                    StreetAddress = hotel.StreetAddress,
                    City = hotel.City,
                    State = hotel.State,
                    Phone = hotel.Phone,
                    Country = hotel.Country
                }).FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task Delete(int id)
        {
            Hotel hotel = await _context.Hotels.FindAsync(id);
            _context.Entry(hotel).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}
