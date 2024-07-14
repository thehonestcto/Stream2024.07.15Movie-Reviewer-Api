namespace MovieReviewer.Api.Shared.Dtos
{
    public class ReviewViewModel
    {
        public required string ReviewContent { get; set; }
        public int ReviewScore { get; set; }
        public int MovieId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
