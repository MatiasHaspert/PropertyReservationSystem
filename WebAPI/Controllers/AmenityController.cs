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
            try
            {
                await _amenityService.UpdateAmenityAsync(id, amenity);
                return NoContent();
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
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
            try
            {
                await _amenityService.DeleteAmenityAsync(id);
                return NoContent();
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }
    }
}
