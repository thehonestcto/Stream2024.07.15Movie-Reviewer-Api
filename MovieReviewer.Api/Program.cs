using MovieReviewer.Api.control.repository;
using MovieReviewer.Api.control.services.imdb;
using MovieReviewer.Api.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.Configure<Settings>(builder.Configuration.GetSection("OmDbSettings"));
builder.Services.AddHttpClient<ImdbS>(client => { client.BaseAddress = new Uri("https://www.omdbapi.com/"); });

builder.Services.AddTransient<MovieR, MovieR>();
builder.Services.AddScoped<ReviewR, ReviewR>();
builder.Services.AddScoped<ImdbS, ImdbS>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
