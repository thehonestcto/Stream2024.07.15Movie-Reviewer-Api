namespace MovieReviewer.Api.Shared
{
    public class ResponseFromService
    {
        public bool IsSuccess { get; set; }
        //TODO: Change this later
        public List<string> Errors { get; set; }
    }

    public class ResponseFromService<T> : ResponseFromService
    {
        public T Data { get; set; }
    }
}
