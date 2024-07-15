using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using MovieReviewer.Api.Data;
using MovieReviewer.Api.Shared.Dtos;
using MovieReviewer.Api.Shared.Helpers;

namespace MovieReviewer.Api.Features.Movie
{
    public class MovieService : IMovieService
    {
        private readonly ApplicationDbContext _context;
        private readonly OmDbClient _omDbClient;
        private readonly IMovieClient _movieClient;
        public MovieService(ApplicationDbContext context, IMovieClient movieClient)
        {
            _context = context;
            _movieClient = movieClient;
        }

        public async Task<Result<int>> CreateMovie(string imdbId)
        {
            //check if imdbid is in the db
            if (await _context.Movies.FirstOrDefaultAsync(x => x.ImdbId == imdbId) is not null)
                return Result.Conflict();

            //call external api
            var responseFromClient = await _movieClient.GetMovieInfo(imdbId);
            if (!responseFromClient.IsSuccess)
                return Result.NotFound();

            var item = responseFromClient.Value.ParseMovieData();
            await _context.Movies.AddAsync(item);
            await _context.SaveChangesAsync();
            return Result.Success(item.Id);
        }

        public async Task<Result<MovieViewModel>> GetMovieById(int movieId)
        {
            var item = await _context.Movies.FirstOrDefaultAsync(x => x.Id == movieId);
            return item is null ? Result.NotFound() : Result.Success(item.ToMovieViewModel());
        }

        public async Task<Result<List<MovieViewModel>>> GetAllMovies()
        {
            var items = await _context.Movies.Select(x => x.ToMovieViewModel()).ToListAsync();
            return Result.Success(items);
        }

        public async Task<Result> DeleteMovie(int movieId)
        {
            var item = await _context.Movies.FirstOrDefaultAsync(x => x.Id == movieId);
            if (item is null)
                return Result.NotFound();
            
            item.IsDisabled = true;
            item.LastUpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return Result.NoContent();
        }
    }
}