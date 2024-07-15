using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using MovieReviewer.Api.Features.Movie;
using MovieReviewer.Api.Shared.Dtos;
using System.ComponentModel.DataAnnotations;

namespace MovieReviewer.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [TranslateResultToActionResult]

    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpPost]
        public async Task<Result<int>> CreateMovie([Required] string imdbId)
        {
            return await _movieService.CreateMovie(imdbId);
        }

        [HttpGet]
        public async Task<Result<List<MovieViewModel>>> GetAllMovies()
        {
            return await _movieService.GetAllMovies();
        }

        [HttpGet("{id}")]
        public async Task<Result<MovieViewModel>> GetMovieData(int id)
        {
            return await _movieService.GetMovieById(id);
        }


        [HttpDelete("{id}")]
        public async Task<Result> DeleteMovieData(int id)
        {
            return await _movieService.DeleteMovie(id);
        }
    }
}
