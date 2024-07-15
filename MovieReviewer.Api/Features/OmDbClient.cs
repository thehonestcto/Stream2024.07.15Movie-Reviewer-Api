using System.Web;
using Ardalis.Result;
using Microsoft.Extensions.Options;
using MovieReviewer.Api.Domain;
using MovieReviewer.Api.Features.Movie;
using MovieReviewer.Api.Shared;
using MovieReviewer.Api.Shared.Settings;
using Newtonsoft.Json;

namespace MovieReviewer.Api.Features
{
    public class OmDbClient : IMovieClient
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<OmDbSettings> _settings;  
        public OmDbClient(HttpClient httpClient, IOptions<OmDbSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings;
        }
        public async Task<Result<MovieInformation>> GetMovieInfo(string imdbId)
        {
            var result = await GenerateDataFromExternalApi(imdbId);
            if (!result.IsSuccess)
                return Result.NotFound();

            return Result.Success(new MovieInformation
            {
                Title = result.Value.Title,
                Rated = result.Value.Rated,
                Plot = result.Value.Plot,
                Language = result.Value.Language,
                ImDbRating = result.Value.ImDbRating,
                ImDbId = result.Value.ImDbId
            });
        }
        private async Task<Result<OmDbMovieDataResponse>> GenerateDataFromExternalApi(string imdbId)
        {
            var url = $"?i={imdbId}&apikey={_settings.Value.ApiKey}";
            var apiResponse = await _httpClient.GetAsync(url);
            var result = await ParseRawDataIntoObjects(apiResponse.Content);
            return result.IsSuccess ? result : Result.Error();
        }

        /*TODO:
            The problem here is that there is double quota
         */
        private async Task<Result<OmDbMovieDataResponse>> ParseRawDataIntoObjects(HttpContent content)
        {
            var contentText = await content.ReadAsStringAsync();
            try
            {
                var response = JsonConvert.DeserializeObject<OmDbResponse>(contentText);
                if (response.Response != "True") return Result.NotFound();
                var movieDataRaw = JsonConvert.DeserializeObject<OmDbMovieDataResponse>(contentText);
                return Result.Success(movieDataRaw);
            }
            catch (JsonException exception)
            {
                return Result.NotFound();
            }
        }
    }
}
