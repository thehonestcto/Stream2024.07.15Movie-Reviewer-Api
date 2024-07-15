using MovieReviewer.Api.control.services.imdb.Enums;

namespace MovieReviewer.Api.control {
    public class MovieViewModel {
        public int Id { get; set; }
        public required string Title { get; set; }
        public RatingSystem MovieRating { get; set; }
        public Language MovieLanguage { get; set; }
        public required string ImdbId { get; set; }
        public required double ImdbRating { get; set; }
    }
}
