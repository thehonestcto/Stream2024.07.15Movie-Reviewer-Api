using MovieReviewer.Api.Domain.Enums;

namespace MovieReviewer.Api.Shared.Dtos
{
    public class MovieDto
    {
        public required string Title { get; set; }
        public RatingSystem MovieRating { get; set; }
        public Language MovieLanguage { get; set; }
        public required string ImdbId { get; set; }
        public required double ImdbRating { get; set; }
        public bool IsDeleted { get; set; }
    }
}
