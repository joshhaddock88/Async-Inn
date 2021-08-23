using AsyncInn.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models.Interfaces
{
    public interface IRoom
    {
        // CREATE
        Task<RoomDTO> Create(NewRoomDTO paramRoom);

        // GET ALL
        Task<List<RoomDTO>> GetRooms();

        // GET ONE BY ID
        Task<RoomDTO> GetRoom(int id);

        // UPDATE
        Task<Room> UpdateRoom(int id, Room room);

        Task AddAmenity(int roomId, int amenityId);
        Task RemoveAmenityFromRoom(int roomId, int amenityId);

        // DELETE
        Task Delete(int id);
    }
}
