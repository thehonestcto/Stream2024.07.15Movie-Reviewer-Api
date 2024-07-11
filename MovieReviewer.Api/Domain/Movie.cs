using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.EntityFrameworkCore;
using MovieReviewer.Api.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace MovieReviewer.Api.Domain
{
    [Index(nameof(ImdbId), IsUnique = true)]
    public class Movie : BaseEntity
    {
        [Required]
        public required string Title { get; set; }
        [Required]
        public RatingSystem MovieRating { get; set; }
        [Required]
        public Language MovieLanguage { get; set; }
        [Required]
        public required string ImdbId { get; set; }
        [Required]
        public required double ImdbRating { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime LastUpdatedAt { get; set; }
        public bool IsDisabled { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
