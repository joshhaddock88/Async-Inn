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
    public class AmenityService : IAmenity
    {

        private AsyncInnDbContext _context;

        public AmenityService(AsyncInnDbContext context)
        {
            _context = context;
        }

        public async Task<AmenityDTO> Create(NewAmenityDTO amenity)
        {
            Amenity newAmenity = new Amenity()
            {
                Name = amenity.Name
            };
            
            _context.Entry(newAmenity).State = EntityState.Added;

            AmenityDTO addedAmenity = new AmenityDTO()
            {
                Name = amenity.Name
            };

            await _context.SaveChangesAsync();
            return addedAmenity;
        }

        public async Task Delete(int id)
        {
            Amenity amenity = await _context.Amenities.FindAsync(id);
            _context.Entry(amenity).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<List<AmenityDTO>> GetAmenities()
        {
            return await _context.Amenities
                .Select(amenity => new AmenityDTO
                {
                    Id = amenity.Id,
                    Name = amenity.Name

                }).ToListAsync();
        }

        public async Task<AmenityDTO> GetAmenity(int id)
        {
            return await _context.Amenities
                .Select(amenity => new AmenityDTO
                {
                    Id = amenity.Id,
                    Name = amenity.Name
                }).FirstOrDefaultAsync();
        }

        public async Task<Amenity> UpdateAmenity(int id, Amenity amenity)
        {
            _context.Entry(amenity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return amenity;
        }
    }
}
