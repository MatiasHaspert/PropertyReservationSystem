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
            try
            {
                var reviews = await _ReviewService.GetPropertyReviewsAsync(propertyId);
                return Ok(reviews);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // GET: api/Review/5?propertyId=5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewResponseDTO>> GetReview(int id, [FromQuery] int propertyId)
        {
            try
            {
                var review = await _ReviewService.GetPropertyReviewByIdAsync(propertyId, id);
                return Ok(review);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        
        // PUT: api/Review/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReview(int id, ReviewRequestDTO reviewRequestDTO)
        {
            try
            {
                await _ReviewService.UpdateReviewAsync(id, reviewRequestDTO);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST: api/Review
        [HttpPost]
        public async Task<ActionResult<ReviewResponseDTO>> PostReview(ReviewRequestDTO reviewRequestDTO)
        {
            try
            {
                var createdReview = await _ReviewService.CreateReviewAsync(reviewRequestDTO);
                return CreatedAtAction("GetReview", new { id = createdReview.Id, propertyId = createdReview.PropertyId }, createdReview);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // DELETE: api/Review/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            try
            {
                await _ReviewService.DeleteReviewAsync(id);
                return NoContent();
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
