using Ardalis.Result;
using MovieReviewer.Api.Shared;
using MovieReviewer.Api.Shared.Dtos;

namespace MovieReviewer.Api.Features.Review
{
    public interface IReviewService
    {
        public Task<Result<int>> CreateReview(ReviewInputModel review, int movieId);
        public Task<Result> DeleteReview(int reviewId);
        public Task<Result<ReviewViewModel>> GetReviewById(int reviewId);
        public Task<Result<List<ReviewViewModel>>> GetAllReviews();
    }
}