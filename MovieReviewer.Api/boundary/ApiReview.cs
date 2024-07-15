using System.ComponentModel.DataAnnotations;
using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using Ardalis.Result.FluentValidation;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MovieReviewer.Api.control;
using MovieReviewer.Api.control.repository;

namespace MovieReviewer.Api.boundary {
    [ApiController]
    [Route("review")]
    [TranslateResultToActionResult]
    public class ApiReview(ReviewR reviewR) : Controller {
        private readonly ReviewCreateValidator createValidator = new();
        private readonly ReviewUpdateValidator updateValidator = new();
        
        [HttpPost]
        public async Task<Result<int>> CreateReview(ReviewCreateModel review, [Required] int movieId) {
            var result = await createValidator.ValidateAsync(review);
            if (!result.IsValid)
                return Result.Invalid(result.AsErrors());

            // TODO: rename this later
            return await reviewR.CreateReview(review, movieId);
        }

        [HttpGet("{id}")]
        public async Task<Result<ReviewViewModel>> GetReviewById([Required] int id) {
            return await reviewR.GetReviewById(id);
        }

        [HttpGet]
        public async Task<Result<List<ReviewViewModel>>> GetAllReviews() {
            return await reviewR.GetAllReviews();
        }

        [HttpDelete("{id}")]
        public async Task<Result> DeleteReview([Required] int id) {
            return await reviewR.DeleteReview(id);
        }

        [HttpPut("{id}")]
        public async Task<Result> UpdateReview([Required] int id, ReviewUpdateModel review) {
            var result = await updateValidator.ValidateAsync(review);
            if (!result.IsValid)
                return Result.Invalid(result.AsErrors());

            return await reviewR.UpdateReview(id, review);
        }
    }

    public class ReviewCreateModel {
        public required string ReviewContent { get; set; }
        public int ReviewScore { get; set; }
    }

    public class ReviewUpdateModel : ReviewCreateModel {
        public bool IsDisabled { get; set; }
    }

    class ReviewCreateValidator : AbstractValidator<ReviewCreateModel> {
        public ReviewCreateValidator() {
            RuleFor(x => x.ReviewContent).NotEmpty();
            RuleFor(x => x.ReviewScore).InclusiveBetween(1, 5);
        }
    }

    class ReviewUpdateValidator : AbstractValidator<ReviewUpdateModel> {
        public ReviewUpdateValidator() {
            Include(new ReviewCreateValidator());
            RuleFor(x => x.IsDisabled).NotNull();
        }
    }
}
