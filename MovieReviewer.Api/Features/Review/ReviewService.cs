using Microsoft.EntityFrameworkCore;
using MovieReviewer.Api.Data;
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

        public async Task<ResponseFromService<List<Domain.Review>>> GetAllReviewsAsync()
        {
            var items = await _context.Reviews.Where(x => x.IsDisabled == false).ToListAsync();
            if (items.Count > 0)
            {
                return new ResponseFromService<List<Domain.Review>> { IsSuccess = true, Data = items };
            }

            return new ResponseFromService<List<Domain.Review>> { IsSuccess = false, Errors = new List<string> { "No items in reviews" } }; 
        }

        public async Task<ResponseFromService<int>> CreateReview(ReviewDto review, int movieId)
        {
            if (await _context.Movies.FirstOrDefaultAsync(x => x.Id == movieId) is null)
            {
                return new ResponseFromService<int> { IsSuccess = false, Errors = new List<string> { "Movie isn't in the db" } };
            }

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

        public async Task<ResponseFromService<int>> DeleteReview(int id)
        {
            var itemById = await _context.Reviews.FirstOrDefaultAsync(x => x.Id == id);
            if (itemById is null)
            {
                return new ResponseFromService<int> { IsSuccess = false, Errors = new List<string> { "Review doesn't exist" } };
            }

            itemById.IsDisabled = true;
            await _context.SaveChangesAsync();
            return new ResponseFromService<int> { IsSuccess = true , Data = itemById.Id };
        }

        public async Task<ResponseFromService<Domain.Review>> GetById(int id)
        {
            var itemById = await _context.Reviews.FirstOrDefaultAsync(x => x.Id == id);
            if (itemById is null)
            {
                return new ResponseFromService<Domain.Review> { IsSuccess = false, Errors = new List<string> { "Review isn't there" } };
            }

            if (itemById.IsDisabled)
            {
                return new ResponseFromService<Domain.Review> { IsSuccess = false, Errors = new List<string> { "Review isn't there" } };
            }

            return new ResponseFromService<Domain.Review> {IsSuccess = true, Data = itemById};
        }
    }
}
