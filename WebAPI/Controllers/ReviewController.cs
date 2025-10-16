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
using PropertyReservation.Application.DTOs.Review;

namespace PropertyReservation.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _ReviewService;

        public ReviewController(IReviewService ReviewService)
        {
            _ReviewService = ReviewService;
        }

        // GET: api/Review?propertyId=5
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewResponseDTO>>> GetPropertyReviews([FromQuery] int propertyId)
        {
            var Reviews = await _ReviewService.GetPropertyReviewsAsync(propertyId);

            if (Reviews == null)
                return NotFound($"No se encontraron Reviews para la property con ID {propertyId}.");

            return Ok(Reviews);
        }


        // GET: api/Review/5?propertyId=5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewResponseDTO>> GetReview(int id, [FromQuery] int propertyId)
        {
            var review = await _ReviewService.GetPropertyReviewByIdAsync(propertyId, id);

            if (review == null)
            {
                return NotFound();
            }

            return review;
        }

        
        // PUT: api/Review/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReview(int id, ReviewRequestDTO reviewRequestDTO)
        {
            var result = await _ReviewService.UpdateReviewAsync(id, reviewRequestDTO);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        // POST: api/Review
        [HttpPost]
        public async Task<ActionResult<ReviewResponseDTO>> PostReview(ReviewRequestDTO reviewRequestDTO)
        {
            var createdReview = await _ReviewService.CreateReviewAsync(reviewRequestDTO);
            return CreatedAtAction("GetReview", new { id = createdReview.Id, propertyId = createdReview.PropertyId }, createdReview);
        }

        // DELETE: api/Review/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var result = await _ReviewService.DeleteReviewAsync(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
