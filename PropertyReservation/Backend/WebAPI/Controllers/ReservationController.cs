using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Domain.Entities;
using Backend.Infrastructure.Data;
using Backend.Application.Interfaces;
using Backend.Application.DTOs.Reservation;

namespace Backend.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        // GET: api/Reservation/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReservationResponseDTO>> GetReservationById(int id)
        {
            try
            {
                return await _reservationService.GetReservationByIdAsync(id);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Reservation/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservation(int id, ReservationRequestDTO reservationDTO)
        {
            try
            {
                await _reservationService.UpdateReservationAsync(id, reservationDTO);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Reservation
        [HttpPost]
        public async Task<ActionResult<ReservationResponseDTO>> PostReservation(ReservationRequestDTO reservationDTO)
        {
            try
            {
                var resertvationResponse = await _reservationService.CreateReservationAsync(reservationDTO);
                return CreatedAtAction("GetReservation", new { id = resertvationResponse.Id }, resertvationResponse);
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }

        }


        // Luego agregar un endpoint para cambiar el estado de una reserva
        // Luego implentarlo cuando se agregue la funcionalidad de usuarios

        // DELETE: api/Reservation/5
        // Endpoint para cancelar una reserva, luego implentarlo cuando se agregue la funcionalidad de usuarios
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
           throw new NotImplementedException();
        }
       
    }
}
