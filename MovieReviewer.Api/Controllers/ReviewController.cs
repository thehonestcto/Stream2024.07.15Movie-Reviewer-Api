using Microsoft.AspNetCore.Mvc;
using MovieReviewer.Api.Features.Review;
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
            var tie = await _reviewService.CreateReview(reivewItem, movieId);
            if (tie.IsSuccess)
            {
                return Ok(new { tie.Data });
            }
            return BadRequest("Couldn't create review");
        }
    }
}
