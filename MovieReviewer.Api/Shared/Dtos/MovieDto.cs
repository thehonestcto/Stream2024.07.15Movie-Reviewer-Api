using Microsoft.EntityFrameworkCore;
using MovieReviewer.Api.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace MovieReviewer.Api.Shared.Dtos
{
    [Index(nameof(ImdbId), IsUnique = true)]
    public class MovieDto
    {
        [Required]
        public required string Title { get; set; }
        [Required]
        public RatingSystem MovieRating { get; set; }
        [Required]
        public Language MovieLanguage { get; set; }
        public required string ImdbId { get; set; }
        [Required]
        public required double ImdbRating { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
    }
}
