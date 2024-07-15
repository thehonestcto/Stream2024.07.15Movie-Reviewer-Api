using System.ComponentModel.DataAnnotations;
using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using MovieReviewer.Api.control;
using MovieReviewer.Api.control.repository;

namespace MovieReviewer.Api.boundary {
    [ApiController]
    [Route("movie")]
    [TranslateResultToActionResult]
    public class ApiMovie(MovieR movieR) : Controller {
        [HttpPost]
        public async Task<Result<int>> CreateMovie([Required] string imdbId) {
            return await movieR.CreateMovie(imdbId);
        }

        [HttpGet]
        public async Task<Result<List<MovieViewModel>>> GetAllMovies() {
            return await movieR.GetAllMovies();
        }

        [HttpGet("{id}")]
        public async Task<Result<MovieViewModel>> GetMovieData(int id) {
            return await movieR.GetMovieById(id);
        }


        [HttpDelete("{id}")]
        public async Task<Result> DeleteMovieData(int id) {
            return await movieR.DeleteMovie(id);
        }
    }
}
