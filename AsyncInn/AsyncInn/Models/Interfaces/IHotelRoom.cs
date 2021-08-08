using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models.Interfaces
{
    public interface IHotelRoom
    {
        // Create
        Task<HotelRoom> Create(HotelRoom hotelRoom);

        // GET ALL

        Task<List<HotelRoom>> GetHotelRooms();

        // GET ONE BY ID
        Task<HotelRoom> GetHotelRoom(int hotelId, int roomId);

        // UPDATE
        Task<HotelRoom> UpdateHotelRoom(int hoteldId, int roomId, HotelRoom hotel);

        // DELETE
        Task DeleteHotelRoom(int hotelId, int roomId);
    }
}
