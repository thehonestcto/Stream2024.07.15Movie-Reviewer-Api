namespace MovieReviewer.Api.Shared.Dtos
{
    public class ReviewInputModel
    {
        public required string ReviewContent { get; set; }
        public int ReviewScore { get; set; }
    }
}
