using Microsoft.AspNetCore.Mvc;
using MovieReviewer.Api.Features.Movie;

namespace MovieReviewer.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMovie(string imdbId)
        {
            var response = await _movieService.CreateMovie(imdbId);

            if (response.IsSuccess)
            {
                return Ok(new {response.Data});
            }

            return BadRequest(response.Errors);
        }

        [HttpGet]
        public async Task<IActionResult> GetMovieData(int id)
        {
            var response = await _movieService.GetMovieData(id);

            if (response.IsSuccess)
            {
                return Ok(response.Data);
            }

            return BadRequest(response.Errors);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMovieData(int id)
        {
            //
            var response = await _movieService.DeleteMovie(id);
            if (response.IsSuccess)
            {
                return NoContent();
            }

            return BadRequest(response.Errors);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateMovieData(int id)
        {

        }
    }
}
