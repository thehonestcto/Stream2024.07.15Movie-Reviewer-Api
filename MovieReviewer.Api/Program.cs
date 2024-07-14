using FluentValidation;
using MovieReviewer.Api.Data;
using MovieReviewer.Api.Features;
using MovieReviewer.Api.Features.Movie;
using MovieReviewer.Api.Features.Review;
using MovieReviewer.Api.Shared.Dtos;
using MovieReviewer.Api.Shared.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.Configure<OmDbSettings>(builder.Configuration.GetSection(nameof(OmDbSettings)));
builder.Services.AddHttpClient<OmDbClient>(client =>
{
    client.BaseAddress = new Uri("https://www.omdbapi.com/");
});

builder.Services.AddTransient<IMovieService, MovieService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IValidator<ReviewInputModel>, ReviewInputValidator>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
