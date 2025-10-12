using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservaPropiedades.Application.Interfaces;
using ReservaPropiedades.Domain.Entities;
using ReservaPropiedades.Infrastructure.Data;
using ReservaPropiedades.Application.DTOs.Propiedad;

namespace ReservaPropiedades.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController] // Gracias a esta anotacion, el controlador responde a solicitudes HTTP, maneja automáticamente la serialización, deserialización de JSON y chequea validaciones.
    public class PropiedadController : ControllerBase
    {
        private readonly IPropiedadService _propiedadService;

        public PropiedadController(IPropiedadService propiedadService)
        {
            _propiedadService = propiedadService;
        }

        // GET: api/Propiedad
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Propiedad>>> GetPropiedades()
        {
            IEnumerable<Propiedad> propiedades = await _propiedadService.GetAllPropiedadesAsync();

            return Ok(propiedades);
        }

        // GET: api/Propiedad/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Propiedad>> GetPropiedad(int id)
        {
            var propiedad = await _propiedadService.GetPropiedadByIdAsync(id);

            if (propiedad == null)
            {
                return NotFound();
            }

            return propiedad;
        }

        // PUT: api/Propiedad/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPropiedad(int id, PropiedadRequestDTO propiedad)
        {
            var result = await _propiedadService.PutPropiedadAsync(id, propiedad);

            if (!result)
            {
                return BadRequest();
            }

            return NoContent();
        }

        // POST: api/Propiedad
        [HttpPost]
        public async Task<ActionResult<Propiedad>> PostPropiedad(PropiedadRequestDTO propiedadDTO)
        {
            Propiedad propiedad = await _propiedadService.CreatePropiedadAsync(propiedadDTO);

            return CreatedAtAction("GetPropiedad", new { id = propiedad.Id }, propiedad);
        }

        // DELETE: api/Propiedad/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePropiedad(int id)
        {
            var result = await _propiedadService.DeletePropiedadAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}
