using MovieReviewer.Api.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace MovieReviewer.Api.Shared.Dtos
{
    public class PUTMovieDto
    {
        [Required]
        public required string Title { get; set; }
        [Required]
        public RatingSystem MovieRating { get; set; }
        [Required]
        public Language MovieLanguage { get; set; }
        [Required]
        public required double ImdbRating { get; set; }
        [Required]
        public bool IsDisabled { get; set; }
    }
}
