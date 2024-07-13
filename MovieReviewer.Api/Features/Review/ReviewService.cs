using Microsoft.EntityFrameworkCore;
using MovieReviewer.Api.Data;
using MovieReviewer.Api.Domain;
using MovieReviewer.Api.Shared;
using MovieReviewer.Api.Shared.Dtos;

namespace MovieReviewer.Api.Features.Review
{
    public class ReviewService : IReviewService
    {
        private readonly ApplicationDbContext _context;
        public ReviewService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Domain.Review>> GetAllReviewsAsync()
        {
            return await _context.Reviews.ToListAsync();
        }

        public async Task<ResponseFromService<int>> CreateReview(ReviewDto review, int movieId)
        {
            var item = new Domain.Review
            {
                MovieId = movieId,
                ReviewContent = review.ReviewContent,
                ReviewScore = review.ReviewScore,

            };

            await _context.Reviews.AddAsync(item);
            await _context.SaveChangesAsync();
            return new ResponseFromService<int> { IsSuccess = true, Data = item.Id };
        }

        public Task UpdateRview()
        {
            throw new NotImplementedException();
        }

        public Task DeleteReview()
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Review> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Domain.Review>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
