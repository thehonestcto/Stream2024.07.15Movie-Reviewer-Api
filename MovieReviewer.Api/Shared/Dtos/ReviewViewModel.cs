namespace MovieReviewer.Api.Shared.Dtos
{
    public class ReviewViewModel : ReviewCreateModel
    {
        public int MovieId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
