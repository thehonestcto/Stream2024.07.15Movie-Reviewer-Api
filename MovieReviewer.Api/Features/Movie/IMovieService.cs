using MovieReviewer.Api.Domain;
using MovieReviewer.Api.Shared;
using MovieReviewer.Api.Shared.Dtos;

namespace MovieReviewer.Api.Features.Movie
{
    public interface IMovieService
    {
        public Task<ResponseFromService<int>> CreateMovie(string imdbId);
        public Task<ResponseFromService<MovieDto>> GetMovieData(int movieId);
        public Task<ResponseFromService> DeleteMovie(int movieId);
    }
}
