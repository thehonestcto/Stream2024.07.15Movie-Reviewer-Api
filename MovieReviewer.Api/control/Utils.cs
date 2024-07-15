using MovieReviewer.Api.control.services.imdb;
using MovieReviewer.Api.control.services.imdb.Enums;

namespace MovieReviewer.Api.control {
    public static class Utils {
        private static Language ParseMeAMovieLanguage(string language) {
            return language.ToLower() switch {
                "english" => Language.En,
                "german" or "deutsche" => Language.De,
                "french" => Language.Fr,
                "korean" => Language.Ko,
                "hindi" => Language.Hi,
                "arabic" => Language.Ar,
                "japanese" => Language.Ja,
                "chinese" => Language.Zh,
                _ => Language.Other
            };
        }

        private static RatingSystem ParseMeAMovieRating(string rated) {
            return rated.ToLower() switch {
                "g" => RatingSystem.White,
                "pg" => RatingSystem.Yellow,
                "pg-13" => RatingSystem.Purple,
                "r" => RatingSystem.Red,
                "nc-17" => RatingSystem.Black,
                _ => RatingSystem.NotRated
            };
        }


        public static Movie ParseMovieData(this ImdbMovie RawMovieData) {
            var movieData = new Movie {
                Title = RawMovieData.Title,
                ImdbId = RawMovieData.ImDbId,
                ImdbRating = double.Parse(RawMovieData.ImDbRating),
                IsDisabled = false,
                LastUpdatedAt = DateTime.UtcNow,
                MovieRating = ParseMeAMovieRating(RawMovieData.Rated),
                MovieLanguage = ParseMeAMovieLanguage(RawMovieData.Language),
                Plot = RawMovieData.Plot,
            };
            return movieData;
        }
    }
}
