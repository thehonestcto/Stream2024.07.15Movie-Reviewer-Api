using System.ComponentModel.DataAnnotations;

namespace MovieReviewer.Api.Domain
{
    public class Review : BaseEntity
    {
        [Required]
        public required string ReviewContent { get; set; }
        [Range(0, 6)]
        public int ReviewScore { get; set; }
        public int MovieId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; } = false;
    }
}
