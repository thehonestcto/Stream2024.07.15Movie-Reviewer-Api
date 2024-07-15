using Ardalis.Result;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace MovieReviewer.Api.control.services.imdb {
    public class ImdbS(
        HttpClient httpClient,
        IOptions<Settings> settings
    ) {
        public async Task<Result<ImdbMovie>> GetMovieInfo(string imdbId) {
            var result = await GenerateDataFromExternalApi(imdbId);
            if (!result.IsSuccess)
                return Result.NotFound();

            return Result.Success(new ImdbMovie {
                Title = result.Value.Title,
                Rated = result.Value.Rated,
                Plot = result.Value.Plot,
                Language = result.Value.Language,
                ImDbRating = result.Value.ImDbRating,
                ImDbId = result.Value.ImDbId
            });
        }

        private async Task<Result<RespMovie>> GenerateDataFromExternalApi(string imdbId) {
            var url = $"?i={imdbId}&apikey={settings.Value.ApiKey}";
            var apiResponse = await httpClient.GetAsync(url);
            var result = await ParseRawDataIntoObjects(apiResponse.Content);
            return result.IsSuccess ? result : Result.Error();
        }

        // TODO: The problem here is that there is double quota
        private async Task<Result<RespMovie>> ParseRawDataIntoObjects(HttpContent content) {
            var contentText = await content.ReadAsStringAsync();
            try {
                var response = JsonConvert.DeserializeObject<Resp>(contentText);
                if (response.Response != "True") return Result.NotFound();
                var movieDataRaw = JsonConvert.DeserializeObject<RespMovie>(contentText);
                return Result.Success(movieDataRaw);
            } catch (JsonException exception) {
                return Result.NotFound();
            }
        }
    }

    public class Settings {
        public string ApiKey { get; set; }
    }

    class Resp {
        public string Response { get; set; }
    }

    class RespError : Resp {
        public string Error { get; set; }
    }

    class RespMovie : Resp {
        public required string Title { get; set; }
        public required string Year { get; set; }
        public required string Rated { get; set; }
        public required string Plot { get; set; }
        public required string Language { get; set; }
        public required string ImDbRating { get; set; }
        public required string ImDbId { get; set; }
    }
}
