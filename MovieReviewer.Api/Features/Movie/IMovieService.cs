using Ardalis.Result;
using MovieReviewer.Api.Shared.Dtos;

namespace MovieReviewer.Api.Features.Movie
{
    public interface IMovieService
    {
        public Task<Result<int>> CreateMovie(string imdbId);
        public Task<Result<MovieViewModel>> GetMovieData(int movieId);
        public Task<Result<List<MovieViewModel>>> GetAllMovieData();
        public Task<Result> DeleteMovie(int movieId);
    }
}
