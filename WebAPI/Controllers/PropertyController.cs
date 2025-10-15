using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PropertyReservation.Application.Interfaces;
using PropertyReservation.Domain.Entities;
using PropertyReservation.Infrastructure.Data;
using PropertyReservation.Application.DTOs.Property;

namespace PropertyReservation.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController] // Gracias a esta anotacion, el controlador responde a solicitudes HTTP, maneja automáticamente la serialización, deserialización de JSON y chequea validaciones.
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyService _PropertyService;

        public PropertyController(IPropertyService PropertyService)
        {
            _PropertyService = PropertyService;
        }

        // GET: api/Property
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Property>>> GetProperties()
        {
            IEnumerable<Property> propertyes = await _PropertyService.GetAllPropertiesAsync();

            return Ok(propertyes);
        }

        // GET: api/Property/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Property>> GetProperty(int id)
        {
            var property = await _PropertyService.GetPropertyByIdAsync(id);

            if (property == null)
            {
                return NotFound();
            }

            return property;
        }

        // PUT: api/Property/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProperty(int id, PropertyRequestDTO property)
        {
            var result = await _PropertyService.PutPropertyAsync(id, property);

            if (!result)
            {
                return BadRequest();
            }

            return NoContent();
        }

        // POST: api/Property
        [HttpPost]
        public async Task<ActionResult<Property>> PostProperty(PropertyRequestDTO propertyDTO)
        {
            Property property = await _PropertyService.CreatePropertyAsync(propertyDTO);

            return CreatedAtAction("GetProperty", new { id = property.Id }, property);
        }

        // DELETE: api/Property/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProperty(int id)
        {
            var result = await _PropertyService.DeletePropertyAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}
