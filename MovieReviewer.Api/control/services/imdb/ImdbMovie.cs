﻿namespace MovieReviewer.Api.control.services.imdb;

public class ImdbMovie {
    public required string Title { get; set; }
    public required string Rated { get; set; }
    public required string Plot { get; set; }
    public required string Language { get; set; }
    public required string ImDbRating { get; set; }
    public required string ImDbId { get; set; }
}
