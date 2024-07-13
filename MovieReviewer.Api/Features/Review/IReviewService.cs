using MovieReviewer.Api.Shared;
using MovieReviewer.Api.Shared.Dtos;

namespace MovieReviewer.Api.Features.Review
{
    public interface IReviewService
    {
        public Task<ResponseFromService<int>> CreateReview(ReviewDto review, int movieId);
        public Task UpdateRview();
        public Task DeleteReview();
        public Task<Domain.Review> GetById(int id);
        public Task<List<Domain.Review>> GetAll();
    }
}
