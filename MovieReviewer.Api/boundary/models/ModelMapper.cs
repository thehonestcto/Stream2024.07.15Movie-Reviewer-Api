using MovieReviewer.Api.boundary;
using MovieReviewer.Api.control.services.imdb;

namespace MovieReviewer.Api.control {
    public static class ModelMapper {
        public static Review ToReviewObject(this ReviewCreateModel review, int movieId) {
            return new Review {
                MovieId = movieId,
                ReviewContent = review.ReviewContent,
                ReviewScore = review.ReviewScore,
            };
        }
    }
}
