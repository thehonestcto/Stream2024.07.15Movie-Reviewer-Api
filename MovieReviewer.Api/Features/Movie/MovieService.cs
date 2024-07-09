using MovieReviewer.Api.Data;
using MovieReviewer.Api.Shared;

namespace MovieReviewer.Api.Features.Movie
{
    public class MovieService : IMovieService
    {
        private readonly ApplicationDbContext _context;
        private readonly OmDbClient _omDbClient;
        public MovieService(ApplicationDbContext context, OmDbClient omDbClient)
        {
            _context = context;
            _omDbClient = omDbClient;
        }

        public async Task<ResponseFromService<int>> CreateMovie(string imdbId)
        {
            var response = await _omDbClient.GetMovieDataFromExternalApi(imdbId);
            if (response.IsSuccess == false)
                return new ResponseFromService<int> { IsSuccess = response.IsSuccess, Errors = response.Errors };
            else
            {
                await _context.Movies.AddAsync(response.Data);
                await _context.SaveChangesAsync();
                return new ResponseFromService<int> { IsSuccess = response.IsSuccess, Data = response.Data.Id };
            }
        }
    }
}
