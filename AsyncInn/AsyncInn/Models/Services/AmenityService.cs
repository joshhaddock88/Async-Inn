using Microsoft.EntityFrameworkCore;
using AsyncInn.Data;
using AsyncInn.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models.Services
{
    public class AmenityService : IAmenity
    {

        private AsyncInnDbContext _context;

        public AmenityService(AsyncInnDbContext context)
        {
            _context = context;
        }

        public async Task<Amenity> Create(Amenity amenity)
        {
            // amenity is an instance of Amenity
            // the current state ofthe amentiy object: raw

            _context.Entry(amenity).State = EntityState.Added;
            // the current state of the amenity object: added

            await _context.SaveChangesAsync();

            return amenity;
        }

        public async Task<List<Amenity>> GetAmenities()
        {
            var amenities = await _context.Amenities.ToListAsync();
            return amenities;
        }

        public async Task<Amenity> GetAmenity(int id)
        {
            Amenity amenity = await _context.Amenities.FindAsync(id);
            return amenity;
        }

        public async Task<Amenity> UpdateAmenity(int id, Amenity amenity)
        {
            _context.Entry(amenity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return amenity;
        }

        public async Task Delete(int id)
        {
            Amenity amenity = await GetAmenity(id);
            _context.Entry(amenity).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}
