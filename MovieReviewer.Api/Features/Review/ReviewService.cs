using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using MovieReviewer.Api.Data;
using MovieReviewer.Api.Domain;
using MovieReviewer.Api.Shared.Dtos;
using MovieReviewer.Api.Shared.Helpers;

namespace MovieReviewer.Api.Features.Review
{
    public class ReviewService : IReviewService
    {
        private readonly ApplicationDbContext _context;
        private readonly ValidationResult _errors;
        public ReviewService(ApplicationDbContext context)
        {
            _context = context;
            _errors = new ValidationResult();
        }

        public async Task<Result<int>> CreateReview(ReviewInputModel review, int movieId)
        {
            //Check if movie is in the Db & Return Failure
            if (await _context.Movies.FirstOrDefaultAsync(x => x.Id == movieId) is null)
            {
                _errors.Errors.Add(new ValidationFailure(nameof(movieId), $"Movie with id {movieId} Not Found"));
                return Result.Invalid(_errors.AsErrors());
            }

            var item = new Domain.Review
            {
                MovieId = movieId,
                ReviewContent = review.ReviewContent,
                ReviewScore = review.ReviewScore,

            };

            await _context.Reviews.AddAsync(item);
            await _context.SaveChangesAsync();
            return Result.Success(item.Id);
        }

        public async Task<Result> DeleteReview(int reviewId)
        {
            var item = await _context.Reviews.FirstOrDefaultAsync(x => x.Id == reviewId);
            if (item is null)
            {
                _errors.Errors.Add(new ValidationFailure(nameof(reviewId), $"Review with id {reviewId} Not Found"));
                return Result.Invalid(_errors.AsErrors());
            }

            item.IsDisabled = true;
            await _context.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result<List<ReviewViewModel>>> GetAllReviews()
        {
            var items = await  _context.Reviews.Select(x => x.ToReviewViewModel()).ToListAsync();
            return Result.Success(items);
        }

        public async Task<Result<ReviewViewModel>> GetReviewById(int reviewId)
        {
            var item = await _context.Reviews.FirstOrDefaultAsync(x => x.Id == reviewId);
            if (item is null)
            {
                _errors.Errors.Add(new ValidationFailure(nameof(reviewId), $"Movie with id {reviewId} Not Found"));
                return Result.Invalid(_errors.AsErrors());
            }

            return Result.Success(item.ToReviewViewModel());
        }
    }
}
