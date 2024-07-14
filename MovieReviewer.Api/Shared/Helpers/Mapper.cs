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
                IsDeleted = movie.IsDisabled,
            };
        }

        public static MovieViewModel ToMovieViewModel(this Movie movie)
        {
            return new MovieViewModel
            {
                Title = movie.Title,
                MovieRating = movie.MovieRating,
                MovieLanguage = movie.MovieLanguage,
                ImdbId = movie.ImdbId,
                ImdbRating = movie.ImdbRating,
            };
        }

        public static ReviewViewModel ToReviewViewModel(this Review review)
        {
            return new ReviewViewModel
            {
                ReviewContent = review.ReviewContent,
                ReviewScore = review.ReviewScore,
                MovieId = review.MovieId,
                CreatedAt = review.CreatedAt,
            };
        }
    }
}
