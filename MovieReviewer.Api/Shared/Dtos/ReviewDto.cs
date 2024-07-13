using System.ComponentModel.DataAnnotations;

namespace MovieReviewer.Api.Shared.Dtos
{
    public class ReviewDto
    {
        [Required]
        public required string ReviewContent { get; set; }
        [Range(0, 6)]
        public int ReviewScore { get; set; }
    }
}
