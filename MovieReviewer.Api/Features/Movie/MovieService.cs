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
        private readonly ValidationResult _error;
        public MovieService(ApplicationDbContext context, OmDbClient omDbClient)
        {
            _context = context;
            _omDbClient = omDbClient;
            _error = new ValidationResult();
        }

        public async Task<Result<int>> CreateMovie(string imdbId)
        {
            //check if imdbid is in the db
            if (await _context.Movies.FirstOrDefaultAsync(x => x.ImdbId == imdbId) is not null)
            {
                _error.Errors.Add(new ValidationFailure(nameof(imdbId), $"Movie with IMDB id {imdbId} exists in the database"));
                return Result.Invalid(_error.AsErrors());
            }

            //call external api
            var response = await _omDbClient.GetMovieDataFromExternalApi(imdbId);
            if (response.IsSuccess == false)
            {
                _error.Errors.Add(new ValidationFailure(nameof(imdbId), $"{response.Errors.First()}"));
                return Result.Invalid(_error.AsErrors());
            }
            
            await _context.Movies.AddAsync(response.Data);
            await _context.SaveChangesAsync();
            return Result.Success(response.Data.Id);
        }

        public async Task<Result> DeleteMovie(int movieId)
        {
            var item = await _context.Movies.FirstOrDefaultAsync(x => x.Id == movieId);
            if (item is null)
            {
                _error.Errors.Add(new ValidationFailure(nameof(movieId), $"Movie with id {movieId} doesn't exists in the database"));
                return Result.Invalid(_error.AsErrors());
            }

            item.IsDisabled = true;
            item.LastUpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result<List<MovieViewModel>>> GetAllMovieData()
        {
            var items = await _context.Movies.Select(x => x.ToMovieViewModel()).ToListAsync();
            return Result.Success(items);
        }

        public async Task<Result<MovieViewModel>> GetMovieData(int movieId)
        {
            var item = await _context.Movies.FirstOrDefaultAsync(x => x.Id == movieId);
            if (item is null)
            {
                _error.Errors.Add(new ValidationFailure(nameof(movieId), $"Movie with id {movieId} doesn't exists in the database"));
                return Result.Invalid(_error.AsErrors());
            }

            return Result.Success(item.ToMovieViewModel());
        }
    }
}