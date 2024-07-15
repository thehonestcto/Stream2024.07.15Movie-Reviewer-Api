namespace MovieReviewer.Api.Shared.Dtos
{
    public class ReviewViewModel : ReviewCreateModel
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
    }
}
