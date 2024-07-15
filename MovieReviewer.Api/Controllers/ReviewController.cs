using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using Ardalis.Result.FluentValidation;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MovieReviewer.Api.Features.Review;
using MovieReviewer.Api.Shared.Dtos;
using System.ComponentModel.DataAnnotations;

namespace MovieReviewer.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [TranslateResultToActionResult]
    public class ReviewController : Controller
    {
        private IReviewService _reviewService;
        private IValidator<ReviewCreateModel> _inputValidator;
        private IValidator<ReviewUpdateModel> _updateValidator;
        public ReviewController(IReviewService reviewService, IValidator<ReviewCreateModel> inputValidator, IValidator<ReviewUpdateModel> updateValidator)
        {
            _reviewService = reviewService;
            _inputValidator = inputValidator;
            _updateValidator = updateValidator;
        }

        [HttpPost]
        public async Task<Result<int>> CreateReview(ReviewCreateModel review, [Required] int movieId)
        {
            var result = await _inputValidator.ValidateAsync(review);
            if (!result.IsValid)
            {
                return Result<int>.Invalid(result.AsErrors());
            }

            //rename this later
            var response = await _reviewService.CreateReview(review, movieId);
            return response;
        }

        [HttpGet("{id}")]
        public async Task<Result<ReviewViewModel>> GetReviewById([Required] int id)
        {
            var response = await _reviewService.GetReviewById(id);
            return response;
        }

        [HttpGet]
        public async Task<Result<List<ReviewViewModel>>> GetAllReviews()
        {
            var response = await _reviewService.GetAllReviews();
            return response;
        }

        [HttpDelete("{id}")]
        public async Task<Result> DeleteReview([Required] int id)
        {
            var response = await _reviewService.DeleteReview(id);
            return response;
        }

        [HttpPut("{id}")]
        public async Task<Result> UpdateReview([Required]int id, ReviewUpdateModel review)
        {
            var result = await _updateValidator.ValidateAsync(review);
            if (!result.IsValid)
            {
                return Result.Invalid(result.AsErrors());
            }

            return await _reviewService.UpdateReview(id, review);
        }
    }
}
