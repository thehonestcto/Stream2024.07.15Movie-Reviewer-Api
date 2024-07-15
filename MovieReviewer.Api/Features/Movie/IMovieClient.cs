using Ardalis.Result;
using MovieReviewer.Api.Domain;

namespace MovieReviewer.Api.Features.Movie;

public interface IMovieClient
{
    Task<Result<MovieInformation>> GetMovieInfo(string imdbId);
}