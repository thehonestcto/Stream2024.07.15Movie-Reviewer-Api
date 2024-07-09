using MovieReviewer.Api.Domain;
using MovieReviewer.Api.Shared.Dtos;

namespace MovieReviewer.Api.Shared.Helpers
{
    public static class Mapper
    {
        public static MovieDto ToMovieDto(this Movie movie)
        {
            return new MovieDto
            {
                Title = movie.Title,
                MovieRating = movie.MovieRating,
                MovieLanguage = movie.MovieLanguage,
                ImdbId = movie.ImdbId,
                ImdbRating = movie.ImdbRating,
                IsDeleted = movie.IsDeleted,
            };
        }
    }
}
