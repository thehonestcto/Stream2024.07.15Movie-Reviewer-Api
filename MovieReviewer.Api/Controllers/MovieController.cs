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
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpPost]
        [TranslateResultToActionResult]
        public async Task<Result<int>> CreateMovie([Required] string imdbId)
        {
            return await _movieService.CreateMovie(imdbId);
        }

        [HttpGet]
        [TranslateResultToActionResult]
        public async Task<Result<List<MovieViewModel>>> GetAllMovies()
        {
            return await _movieService.GetAllMovieData();
        }

        [HttpGet("{id}")]
        [TranslateResultToActionResult]
        public async Task<Result<MovieViewModel>> GetMovieData(int id)
        {
            var response = await _movieService.GetMovieData(id);
            return response;
        }


        [HttpDelete("{id}")]
        [TranslateResultToActionResult]
        public async Task<Result> DeleteMovieData(int id)
        {
            var response = await _movieService.DeleteMovie(id);
            return response;
        }
    }
}
