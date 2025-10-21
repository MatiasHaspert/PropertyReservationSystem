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

        // GET: api/Reservation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservation()
        {
            throw new NotImplementedException();
        }

        // GET: api/Reservation/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reservation>> GetReservation(int id)
        {
            throw new NotImplementedException();
        }

        // PUT: api/Reservation/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservation(int id, Reservation reservation)
        {
            throw new NotImplementedException();
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

        // DELETE: api/Reservation/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            throw new NotImplementedException();

        }
       
    }
}
