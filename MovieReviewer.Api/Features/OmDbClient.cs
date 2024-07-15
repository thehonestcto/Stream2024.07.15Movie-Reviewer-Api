using Microsoft.Extensions.Options;
using MovieReviewer.Api.Shared.Helpers;
using MovieReviewer.Api.Shared;
using MovieReviewer.Api.Shared.Settings;
using Newtonsoft.Json;

namespace MovieReviewer.Api.Features
{
    public class OmDbClient
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<OmDbSettings> _settings;  
        public OmDbClient(HttpClient httpClient, IOptions<OmDbSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings;

        }

        public async Task<ResponseFromService<Domain.Movie>?> GetMovieDataFromExternalApi(string imdbId)
        {
            var url = $"?i={imdbId}&apikey={_settings.Value.ApiKey}".ToString();
            var response = await _httpClient.GetAsync(url);
            var data = await GetRawDataForMe(response.Content);

            switch (data)
            {
                case OmDbErrorResponse test:
                    return new ResponseFromService<Domain.Movie> { IsSuccess = false, Errors = new List<string>() { test.Error } };
                case OmDbMovieDataResponse ThisisRawMovieData:
                    return new ResponseFromService<Domain.Movie> { IsSuccess = true, Data = Parsers.ParsedMovieData(ThisisRawMovieData) };
                default:
                    return null;
            }
        }

        private async Task<OmDbResponse> GetRawDataForMe(HttpContent content)
        {
            var contentText = await content.ReadAsStringAsync();
            var ResponseType = JsonConvert.DeserializeObject<OmDbResponse>(contentText);
            if (ResponseType.Response == "True")
            {
                var movieDataRaw = JsonConvert.DeserializeObject<OmDbMovieDataResponse>(contentText);
                return movieDataRaw;
            }
            else
            {
                var ErrorResponse = JsonConvert.DeserializeObject<OmDbErrorResponse>(contentText);
                return ErrorResponse;
            }
        }
    }
}
