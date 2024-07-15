using Ardalis.Result;
using MovieReviewer.Api.Shared.Dtos;

namespace MovieReviewer.Api.Features.Movie
{
    public interface IMovieService
    {
        public Task<Result<int>> CreateMovie(string imdbId);
        public Task<Result<MovieViewModel>> GetMovieById(int movieId);
        public Task<Result<List<MovieViewModel>>> GetAllMovies();
        public Task<Result> DeleteMovie(int movieId);
    }
}
