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
using AsyncInn.Models.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace AsyncInn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        // _room is a "room service" which uses the actual db context
        private readonly IRoom _room;

        public RoomsController(IRoom r)
        {
            _room = r;
        }

        // GET: api/Rooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomDTO>>> GetRooms()
        {
            // You should count the list ...
            var list = await _room.GetRooms();
            return Ok(list);
        }

        // GET: api/Rooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomDTO>> GetRoom(int id)
        {
            RoomDTO room = await _room.GetRoom(id);
            return room;
        }

        // PUT: api/Rooms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Policy = "updateRoom")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoom(int id, Room room)
        {
            if(id != room.Id)
            {
                return BadRequest();
            }

            var updatedRoom = await _room.UpdateRoom(id, room);

            return Ok(updatedRoom);
        }

        // POST: api/Rooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Policy = "createRoom")]
        [HttpPost]
        public async Task<ActionResult<RoomDTO>> PostRoom(NewRoomDTO room)
        {
            RoomDTO newRoom = await _room.Create(room);

            // Return a 201 Header to browser
            // the body of the request will be us running GetStudent(id);
            return CreatedAtAction("GetRoom", new { id = newRoom.Id }, newRoom);
        }

        //Update Amenities
        [Authorize(Policy = "updateRoom")]
        [HttpPost]
        [Route("{roomId}/{amenityId}")]
        public async Task<IActionResult> AddAmenityToRoom(int roomId, int amenityId)
        {
            await _room.AddAmenityToRoom(roomId, amenityId);
            return NoContent();
        }

        // DELETE: api/Rooms/5
        [Authorize(Policy = "deleteRoom")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            await _room.Delete(id);
            return NoContent();
        }
    }
}
