using MovieReviewer.Api.Domain;
using MovieReviewer.Api.Shared.Dtos;

namespace MovieReviewer.Api.Shared.Helpers
{
    public static class Mapper
    {
        public static MovieViewModel ToMovieViewModel(this Movie movie)
        {
            return new MovieViewModel
            {
                Id = movie.Id,
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
                Id = review.Id,
                ReviewContent = review.ReviewContent,
                ReviewScore = review.ReviewScore,
                MovieId = review.MovieId,
            };
        }

        public static Review ToReviewObject(this ReviewCreateModel review, int movieId)
        {
            return new Review
            {
                MovieId = movieId,
                ReviewContent = review.ReviewContent,
                ReviewScore = review.ReviewScore,
            };
        }
    }
}
