using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PropertyReservation.Domain.Entities;
using PropertyReservation.Application.Interfaces;
using PropertyReservation.Application.DTOs.Amenity;

namespace PropertyReservation.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmenityController : ControllerBase
    {
        private readonly IAmenityService _amenityService;

        public AmenityController(IAmenityService amenityService)
        {
            _amenityService = amenityService;
        }

        // GET: api/Amenity
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AmenityResponseDTO>>> GetAmenities()
        {
           return Ok(await _amenityService.GetAllAmenitiesAsync());
        }

        // PUT: api/Amenity/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAmenity(int id, AmenityRequestDTO amenity)
        {
            var result = await _amenityService.UpdateAmenityAsync(id, amenity);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        // POST: api/Amenity
        [HttpPost]
        public async Task<ActionResult<AmenityResponseDTO>> PostAmenity(AmenityRequestDTO amenityRequestDTO)
        {
            return await _amenityService.CreateAmenityAsync(amenityRequestDTO);
        }

        // DELETE: api/Amenity/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAmenity(int id)
        {
            var result = await _amenityService.DeleteAmenityAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
