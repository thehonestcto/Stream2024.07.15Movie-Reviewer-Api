using Ardalis.Result;
using Microsoft.EntityFrameworkCore;
using MovieReviewer.Api.control.services.imdb;
using MovieReviewer.Api.control.services.imdb.Other;
using MovieReviewer.Api.Data;

namespace MovieReviewer.Api.control.repository {
    public class MovieR(
        ApplicationDbContext context,
        ImdbS movieClient
    ) {
        public async Task<Result<int>> CreateMovie(string imdbId) {
            //check if imdbid is in the db
            if (await context.Movies.FirstOrDefaultAsync(x => x.ImdbId == imdbId) is not null)
                return Result.Conflict();

            //call external api
            var responseFromClient = await movieClient.GetMovieInfo(imdbId);
            if (!responseFromClient.IsSuccess)
                return Result.NotFound();

            var item = responseFromClient.Value.ParseMovieData();
            await context.Movies.AddAsync(item);
            await context.SaveChangesAsync();
            return Result.Success(item.Id);
        }

        public async Task<Result<MovieViewModel>> GetMovieById(int movieId) {
            var item = await context.Movies.FirstOrDefaultAsync(x => x.Id == movieId);
            return item is null ? Result.NotFound() : Result.Success(item.ToMovieViewModel());
        }

        public async Task<Result<List<MovieViewModel>>> GetAllMovies() {
            var items = await context.Movies.Select(x => x.ToMovieViewModel()).ToListAsync();
            return Result.Success(items);
        }

        public async Task<Result> DeleteMovie(int movieId) {
            var item = await context.Movies.FirstOrDefaultAsync(x => x.Id == movieId);
            if (item is null)
                return Result.NotFound();

            item.IsDisabled = true;
            item.LastUpdatedAt = DateTime.UtcNow;
            await context.SaveChangesAsync();
            return Result.NoContent();
        }
    }
}
