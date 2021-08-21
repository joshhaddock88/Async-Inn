using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AsyncInn.Data;
using AsyncInn.Models;
using AsyncInn.Models.Interfaces;

namespace AsyncInn.Controllers
{

    [Route("api/[controller]")]
    [ApiController] 
    public class HotelRoomsController : ControllerBase
    {
        private readonly IHotelRooms _hotelRoom;

        public HotelRoomsController(IHotelRooms hr)
        {
            _hotelRoom = hr;
        }

        // get: api/HotelRoom
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelRooms>>> GetHotelRooms()
        {
            var list = await _hotelRoom.GetHotelRooms();
            return Ok(list);
        }

        // GET: api/HotelRooms/5
        [HttpGet("{hotelId}/{roomId}")]
        public async Task<ActionResult<HotelRooms>> GetHotelRoom(int hotelId, int roomId)
        {
            HotelRooms hotelRoom = await _hotelRoom.GetHotelRoom(hotelId, roomId);
            return hotelRoom;
        }

        // POST: api/HotelRoom
        [HttpPost]
        public async Task<ActionResult<HotelRooms>> PostHotelRoom(HotelRooms hotelRoom)
        {
            await _hotelRoom.Create(hotelRoom);
            //return a 201 Header
            // The body of the request will be us running GetTechnology(id);
            return CreatedAtAction("GetHotelRoom", new { id = hotelRoom.HotelId }, hotelRoom);
        }

        // PUT: api/HotelRooms/5
        [HttpPut("{hotelId}/{roomId}")]
        public async Task<IActionResult> PutHotelRoom(int hotelId, int roomId, HotelRooms hotelRoom)
        {
            var updatedHotelRoom = await _hotelRoom.UpdateHotelRoom(hotelId, roomId, hotelRoom);
            return Ok(updatedHotelRoom);
        }
    }
}
