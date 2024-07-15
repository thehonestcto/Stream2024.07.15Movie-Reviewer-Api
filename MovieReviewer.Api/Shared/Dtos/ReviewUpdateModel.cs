namespace MovieReviewer.Api.Shared.Dtos
{
    public class ReviewUpdateModel : ReviewCreateModel
    {
        public bool IsDisabled { get; set; }
    }
}
