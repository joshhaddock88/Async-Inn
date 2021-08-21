using AsyncInn.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models.Interfaces
{
    public interface IHotel
    {
        // Create
        Task<Hotel> Create(Hotel hotel);

        // GET ALL

        Task<List<HotelDTO>> GetHotels();

        // GET ONE BY ID
        Task<HotelDTO> GetHotel(int id);

        Task<Hotel> GetHotelByName(string name);

        // UPDATE
        Task<Hotel> UpdateHotel(int id, Hotel hotel);

        Task AddRoom(int roomId, int hotelId);

        // DELETE
        Task Delete(int id);
    }
}
