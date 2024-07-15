using MovieReviewer.Api.boundary;

namespace MovieReviewer.Api.control {
    public class ReviewViewModel : ReviewCreateModel {
        public int Id { get; set; }
        public int MovieId { get; set; }
    }
}
