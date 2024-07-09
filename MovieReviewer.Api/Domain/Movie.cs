using Microsoft.AspNetCore.Mvc.ViewEngines;
using MovieReviewer.Api.Domain.Enums;

namespace MovieReviewer.Api.Domain
{
    public class Movie : BaseEntity
    {
        public required string Title { get; set; }
        public RatingSystem MovieRating { get; set; }
        public Language MovieLanguage { get; set; }
        public required string ImdbId { get; set; }
        public required double ImdbRating { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public bool IsDisabled { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
