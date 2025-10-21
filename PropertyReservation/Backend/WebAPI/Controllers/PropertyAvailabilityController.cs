using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Backend.Application.DTOs.PropertyAvailability;
using Backend.Application.Interfaces;
using Backend.Domain.Entities;
using Backend.Infrastructure.Data;

namespace Backend.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyAvailabilityController : ControllerBase
    {
        private readonly IPropertyAvailabilityService _propertyAvailabilityService;

        public PropertyAvailabilityController(IPropertyAvailabilityService propertyAvailabilityService)
        {
            _propertyAvailabilityService = propertyAvailabilityService;
        }

        // GET: api/PropertyAvailability?propertyId=5
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PropertyAvailabilityResponseDTO>>> GetPropertyAvailabilities([FromQuery] int propertyId)
        {
            try
            {
                return Ok(await _propertyAvailabilityService.GetPropertyAvailabilitiesAsync(propertyId));
            }
            catch(ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // PUT: api/PropertyAvailability/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPropertyAvailability(int id, PropertyAvailabilityRequestDTO propertyAvailabilityDTO)
        {
            try
            {
                await _propertyAvailabilityService.UpdatePropertyAvailabilityAsync(id, propertyAvailabilityDTO);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            } catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        // POST: api/PropertyAvailability
        [HttpPost]
        public async Task<ActionResult<PropertyAvailabilityResponseDTO>> PostPropertyAvailability(PropertyAvailabilityRequestDTO propertyAvailabilityDTO)
        {
            try
            {
                var createdAvailability = await _propertyAvailabilityService.CreatePropertyAvailabilityAsync(propertyAvailabilityDTO);
                return CreatedAtAction("GetPropertyAvailabilities", new { propertyId = createdAvailability.PropertyId }, createdAvailability);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }
        
        // DELETE: api/PropertyAvailability/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePropertyAvailability(int id)
        {
            try
            {
                await _propertyAvailabilityService.DeletePropertyAvailabilityAsync(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
