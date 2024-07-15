using MovieReviewer.Api.control;

namespace MovieReviewer.Api.control.services.imdb.Other;

public static class EntityMapper {
    public static MovieViewModel ToMovieViewModel(this Movie movie) {
        return new MovieViewModel {
            Id = movie.Id,
            Title = movie.Title,
            MovieRating = movie.MovieRating,
            MovieLanguage = movie.MovieLanguage,
            ImdbId = movie.ImdbId,
            ImdbRating = movie.ImdbRating,
        };
    }

    public static ReviewViewModel ToReviewViewModel(this Review review) {
        return new ReviewViewModel {
            Id = review.Id,
            ReviewContent = review.ReviewContent,
            ReviewScore = review.ReviewScore,
            MovieId = review.MovieId,
        };
    }
}
