using MovieReviewer.Api.Shared;
using MovieReviewer.Api.Shared.Dtos;

namespace MovieReviewer.Api.Features.Review
{
    public interface IReviewService
    {
        public Task<ResponseFromService<int>> CreateReview(ReviewDto review, int movieId);
        public Task UpdateRview();
        public Task<ResponseFromService<int>> DeleteReview(int id);
        public Task<ResponseFromService<Domain.Review>> GetById(int id);
        public Task<ResponseFromService<List<Domain.Review>>> GetAllReviewsAsync();
    }
}
