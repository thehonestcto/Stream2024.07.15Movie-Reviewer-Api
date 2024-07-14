using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using Ardalis.Result.FluentValidation;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MovieReviewer.Api.Domain;
using MovieReviewer.Api.Features.Review;
using MovieReviewer.Api.Shared;
using MovieReviewer.Api.Shared.Dtos;
using System.ComponentModel.DataAnnotations;

namespace MovieReviewer.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReviewController : Controller
    {
        private IReviewService _reviewService;
        private IValidator<ReviewInputModel> _validator;
        public ReviewController(IReviewService reviewService, IValidator<ReviewInputModel> validator)
        {
            _reviewService = reviewService;
            _validator = validator;
        }

        [HttpPost]
        [TranslateResultToActionResult]
        public async Task<Result<int>> CreateReview(ReviewInputModel review, [Required] int movieId)
        {
            var result = await _validator.ValidateAsync(review);
            if (!result.IsValid)
            {
                return Result<int>.Invalid(result.AsErrors());
            }

            //rename this later
            var response = await _reviewService.CreateReview(review, movieId);
            return response;
        }

        [HttpGet("{id}")]
        [TranslateResultToActionResult]
        public async Task<Result<ReviewViewModel>> GetReviewById(int id)
        {
            var response = await _reviewService.GetReviewById(id);
            return response;
        }

        [HttpGet]
        [TranslateResultToActionResult]
        public async Task<Result<List<ReviewViewModel>>> GetAllReviews()
        {
            var response = await _reviewService.GetAllReviews();
            return response;
        }

        [HttpDelete("{id}")]
        [TranslateResultToActionResult]
        public async Task<Result> DeleteReview(int id)
        {
            var response = await _reviewService.DeleteReview(id);
            return response;
        }
    }
}
