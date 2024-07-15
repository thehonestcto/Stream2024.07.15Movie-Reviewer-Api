namespace MovieReviewer.Api.Shared.Dtos
{
    public class ReviewCreateModel
    {
        public required string ReviewContent { get; set; }
        public int ReviewScore { get; set; }
    }
}
