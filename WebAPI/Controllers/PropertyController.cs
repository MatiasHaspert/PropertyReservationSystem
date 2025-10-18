using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PropertyReservation.Application.DTOs.Property;
using PropertyReservation.Application.Interfaces;
using PropertyReservation.Application.Services;
using PropertyReservation.Domain.Entities;
using PropertyReservation.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace PropertyReservation.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController] // Gracias a esta anotacion, el controlador responde a solicitudes HTTP, maneja automáticamente la serialización, deserialización de JSON y chequea validaciones.
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyService _PropertyService;
        private readonly IPropertyImageService _ImageService;
        public PropertyController(
            IPropertyService PropertyService, 
            IPropertyImageService imageService)
        {
            _PropertyService = PropertyService;
            _ImageService = imageService;
        }

        // GET: api/Property
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Property>>> GetProperties()
        {
            IEnumerable<Property> propertyes = await _PropertyService.GetAllPropertiesAsync();

            return Ok(propertyes);
        }

        // GET: api/Property/list
        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<PropertyListResponseDTO>>> GetPropertyList()
        {
            IEnumerable<PropertyListResponseDTO> propertyes = await _PropertyService.GetPropertyListAsync();

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

        // GET: api/Property/detail/5
        [HttpGet("details/{id}")]
        public async Task<ActionResult<PropertyDetailsResponseDTO>> GetPropertyDetails(int id)
        {
            var property = await _PropertyService.GetPropertyDetailsByIdAsync(id);

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
            await _PropertyService.PutPropertyAsync(id, property);

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
            await _PropertyService.DeletePropertyAsync(id);
          
            return NoContent();
        }

        [HttpPost("{id}/images")]
        public async Task<IActionResult> UploadImages(int id, List<IFormFile> files)
        {
            try
            {
                var images = await _ImageService.UploadPropertyImagesAsync(id, files, Request);
                return Ok(images);

            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}/images")]
        public async Task<IActionResult> GetImages(int id)
        {
            try
            {
                var images = await _ImageService.GetImagesByPropertyAsync(id);
                return Ok(images);
            }
            catch(ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("images/{imageId}")]
        public async Task<IActionResult> DeleteImage(int imageId)
        {
            try
            {
                await _ImageService.DeleteImageAsync(imageId);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{propertyId}/images/{imageId}/main")]
        public async Task<IActionResult> SetMainImage(int propertyId, int imageId)
        {
            try
            {
                var result = await _ImageService.SetMainImageAsync(propertyId, imageId);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }
    }
}
