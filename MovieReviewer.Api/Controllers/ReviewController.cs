using Microsoft.AspNetCore.Mvc;
using MovieReviewer.Api.Domain;
using MovieReviewer.Api.Features.Review;
using MovieReviewer.Api.Shared;
using MovieReviewer.Api.Shared.Dtos;

namespace MovieReviewer.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReviewController : Controller
    {
        private IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateReview(ReviewDto reivewItem, int movieId)
        {
            var response = await _reviewService.CreateReview(reivewItem, movieId);
            return PerformTheReturnType(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReviewById(int id)
        {
            var response = await _reviewService.GetById(id);
            return PerformTheReturnType(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReviews()
        {
            var response = await _reviewService.GetAllReviewsAsync();
            return PerformTheReturnType(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var response = await _reviewService.DeleteReview(id);
            return PerformTheReturnType(response);
        }

        private IActionResult PerformTheReturnType<T>(ResponseFromService<T> response)
        {
            if (response.IsSuccess)
            {
                return Ok(new { response.Data });
            }

            return BadRequest(response.Errors);
        }
    }
}
