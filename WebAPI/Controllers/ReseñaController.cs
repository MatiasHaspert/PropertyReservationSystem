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

namespace ReservaPropiedades.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReseñaController : ControllerBase
    {
        private readonly IReseñaService _reseñaService;

        public ReseñaController(IReseñaService reseñaService)
        {
            _reseñaService = reseñaService;
        }

        // GET: api/Reseña?propiedadId=5
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reseña>>> GetReseñasForProperty([FromQuery] int propiedadId)
        {
            var reseñas = await _reseñaService.GetReseñasForProperty(propiedadId);

            if (reseñas == null)
                return NotFound($"No se encontraron reseñas para la propiedad con ID {propiedadId}.");

            return Ok(reseñas);
        }

        /*
        // GET: api/Reseña/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reseña>> GetReseña(int id)
        {
            var reseña = await _context.Reseñas.FindAsync(id);

            if (reseña == null)
            {
                return NotFound();
            }

            return reseña;
        }

        // PUT: api/Reseña/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReseña(int id, Reseña reseña)
        {
            if (id != reseña.Id)
            {
                return BadRequest();
            }

            _context.Entry(reseña).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReseñaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Reseña
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Reseña>> PostReseña(Reseña reseña)
        {
            _context.Reseñas.Add(reseña);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReseña", new { id = reseña.Id }, reseña);
        }

        // DELETE: api/Reseña/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReseña(int id)
        {
            var reseña = await _context.Reseñas.FindAsync(id);
            if (reseña == null)
            {
                return NotFound();
            }

            _context.Reseñas.Remove(reseña);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReseñaExists(int id)
        {
            return _context.Reseñas.Any(e => e.Id == id);
        }
        */
    }
}
