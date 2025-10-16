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
using System.Runtime.CompilerServices;

namespace PropertyReservation.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController] // Gracias a esta anotacion, el controlador responde a solicitudes HTTP, maneja automáticamente la serialización, deserialización de JSON y chequea validaciones.
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyService _PropertyService;
        private readonly IPropertyImageService _ImageService;
        public PropertyController(IPropertyService PropertyService, IPropertyImageService imageService)
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
    }
}
