using Ardalis.Result;
using Microsoft.EntityFrameworkCore;
using MovieReviewer.Api.boundary;
using MovieReviewer.Api.Data;
using MovieReviewer.Api.control.services.imdb.Other;
using MovieReviewer.Api.control;

namespace MovieReviewer.Api.control.repository {
    public class ReviewR(ApplicationDbContext context) {
        public async Task<Result<int>> CreateReview(ReviewCreateModel review, int movieId) {
            //TODO: I am checking if movie is in the db. Should this even be here? Come back and perform test/write tests
            if (await context.Movies.FirstOrDefaultAsync(x => x.Id == movieId) is null)
                return Result.NotFound();

            var item = review.ToReviewObject(movieId);
            await context.Reviews.AddAsync(item);
            await context.SaveChangesAsync();
            return Result.Success(item.Id);
        }

        public async Task<Result<ReviewViewModel>> GetReviewById(int reviewId) {
            var item = await context.Reviews.FirstOrDefaultAsync(x => x.Id == reviewId);
            return item is null ? Result.NotFound() : Result.Success(item.ToReviewViewModel());
        }

        public async Task<Result<List<ReviewViewModel>>> GetAllReviews() {
            var items = await context.Reviews.Select(x => x.ToReviewViewModel()).ToListAsync();
            return Result.Success(items);
        }

        public async Task<Result> DeleteReview(int reviewId) {
            var item = await context.Reviews.FirstOrDefaultAsync(x => x.Id == reviewId);
            if (item is null)
                return Result.NotFound();

            item.IsDisabled = true;
            await context.SaveChangesAsync();
            return Result.NoContent();
        }

        public async Task<Result> UpdateReview(int reviewId, ReviewUpdateModel update) {
            var item = await context.Reviews.FirstOrDefaultAsync(x => x.Id == reviewId);
            if (item is null)
                return Result.NotFound();

            item.IsDisabled = update.IsDisabled;
            item.ReviewContent = update.ReviewContent;
            item.ReviewScore = update.ReviewScore;
            await context.SaveChangesAsync();
            return Result.NoContent();
        }
    }
}
