using AsyncInn.Data;
using AsyncInn.Models.DTOs;
using AsyncInn.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncInn.Models.Services
{
    public class HotelRoomsService : IHotelRooms
    {
        private AsyncInnDbContext _context;

        public HotelRoomsService(AsyncInnDbContext context)
        {
            _context = context;
        }

        public async Task<HotelRoomDTO> Create(HotelRooms hotelRoom)
        {
            HotelRooms newHotelRooms = hotelRoom;

            HotelRoomDTO newHotelRoomsDTO = await GetHotelRoom(hotelRoom.HotelId, hotelRoom.RoomId);
            return newHotelRoomsDTO;
        }

        public async Task<HotelRoomDTO> GetHotelRoom(int hotelId, int roomId)
        {
            HotelRooms hotelRoom = await _context.HotelRooms.FindAsync(hotelId, roomId);
            return hotelRoom;
        }

        public async Task<List<HotelRoomDTO>> GetHotelRooms()
        {
            var hotelRooms = await _context.HotelRooms.ToListAsync();
            return hotelRooms;
        }

        public async Task<HotelRoomDTO> UpdateHotelRoom(int hotelId, int roomId, HotelRooms hotelRoom)
        {
            _context.Entry(hotelRoom).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return hotelRoom;
        }

        public async Task DeleteHotelRoom(int hotelId, int roomId)
        {
            HotelRooms hotelRoom = await _context.HotelRooms.FindAsync(hotelId, roomId);
            _context.Entry(hotelRoom).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}
