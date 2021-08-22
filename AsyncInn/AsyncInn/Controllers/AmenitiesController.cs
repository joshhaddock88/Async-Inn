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
    public class AmenitiesController : ControllerBase
    {
        // _amenity is "amenity service" which uses the actual db context
        private readonly IAmenity _amenity;

        public AmenitiesController(IAmenity a)
        {
            _amenity = a;
        }

        // GET: api/Amenities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AmenityDTO>>> GetAmenities()
        {
            // You should count the list ...
            var list = await _amenity.GetAmenities();
            return Ok(list);
        }

        // GET: api/Amenities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AmenityDTO>> GetAmenity(int id)
        {
            AmenityDTO amenity = await _amenity.GetAmenity(id);
            return amenity;
        }

        // PUT: api/Amenities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Policy = "updateAmenity")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoom(int id, Amenity amenity)
        {
            if (id != amenity.Id)
            {
                return BadRequest();
            }

            var updatedAmenity = await _amenity.UpdateAmenity(id, amenity);

            return Ok(updatedAmenity);
        }

        // POST: api/Amenities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Policy = "createAmenity")]
        [HttpPost]
        public async Task<ActionResult<AmenityDTO>> PostAmenity(NewAmenityDTO amenity)
        {
            AmenityDTO newAmenity = await _amenity.Create(amenity);

            // Return a 201 Header to browser
            // The body of the request will be us running GetAmenity(id);
            return CreatedAtAction("PostAmenity", new { id = newAmenity.Id }, newAmenity);
        }

        // DELETE: api/Amenities/5
        [Authorize(Policy = "deleteAmenity")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            await _amenity.Delete(id);
            return NoContent();
        }
    }
}
