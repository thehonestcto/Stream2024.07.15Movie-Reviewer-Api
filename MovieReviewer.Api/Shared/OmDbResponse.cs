namespace MovieReviewer.Api.Shared
{
    public class OmDbResponse
    {
        public string Response { get; set; }
    }

    public class OmDbErrorResponse : OmDbResponse
    {
        public string Error { get; set; }
    }

    public class OmDbMovieDataResponse : OmDbResponse
    {
        public required string Title { get; set; }
        public required string Year { get; set; }
        public required string Rated { get; set; }
        public required string Plot { get; set; }
        public required string Language { get; set; }
        public required string ImDbRating { get; set; }
        public required string ImDbId { get; set; }
    }
}
