using AsyncInn.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models.Interfaces
{
    public interface IAmenity
    {
        // Create
        Task<AmenityDTO> Create(NewAmenityDTO amenity);

        // GET ALL

        Task<List<AmenityDTO>> GetAmenities();

        // GET ONE BY ID
        Task<AmenityDTO> GetAmenity(int id);

        // UPDATE
        Task<Amenity> UpdateAmenity(int id, Amenity amenity);

        // DELETE
        Task Delete(int id);
    }
}
