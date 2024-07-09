using MovieReviewer.Api.Domain;
using MovieReviewer.Api.Domain.Enums;

namespace MovieReviewer.Api.Shared.Helpers
{
    public static class Parsers
    {
        public static Language ParseMeAMovieLanguage(string language)
        {
            switch (language.ToLower())
            {
                case "english":
                    return Language.EN;
                case "german":
                case "deutsche":
                    return Language.DE;
                case "french":
                    return Language.FR;
                case "korean":
                    return Language.KO;
                case "hindi":
                    return Language.HI;
                case "arabic":
                    return Language.AR;
                case "japanese":
                    return Language.JA;
                case "chinese":
                    return Language.ZH;
                default:
                    return Language.Other;
            }
        }

        public static RatingSystem ParseMeAMovieRating(string rated)
        {
            switch (rated.ToLower())
            {
                case "g":
                    return RatingSystem.White;
                case "pg":
                    return RatingSystem.Yellow;
                case "pg-13":
                    return RatingSystem.Purple;
                case "r":
                    return RatingSystem.Red;
                case "nc-17":
                    return RatingSystem.Black;
                default:
                    return RatingSystem.NotRated;
            }
        }


        public static Movie ParsedMovieData(OmDbMovieDataResponse thisisRawMovieData)
        {
            var data = new Movie
            {
                Title = thisisRawMovieData.Title,
                ImdbId = thisisRawMovieData.ImDbId,
                ImdbRating = double.Parse(thisisRawMovieData.ImDbRating),
                IsDeleted = false,
                LastUpdatedAt = DateTime.UtcNow,
                MovieRating = Parsers.ParseMeAMovieRating(thisisRawMovieData.Rated),
                MovieLanguage = Parsers.ParseMeAMovieLanguage(thisisRawMovieData.Language),
            };

            return data;
        }
    }
}
