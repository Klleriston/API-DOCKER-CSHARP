using Microsoft.EntityFrameworkCore;
using src.Data;
using src.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<MovieNetContext>(o => o.UseNpgsql(builder.Configuration.GetConnectionString("connection")));
builder.Services.AddScoped<JWTservice>(sp => new JWTservice(builder.Configuration["JWTSettings:SecretKey"]));
builder.Services.AddScoped<IMovieRate, RateService>();
builder.Services.AddScoped<MovieService>();
builder.Services.AddScoped<RateService>();
builder.Services.AddScoped<IUserRepository, UserService>();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(o => o
    .WithOrigins(new[] { "http://localhost:4200", }) ///PORTA DO FRONTEND ANGULAS
    .AllowCredentials()
    .AllowAnyHeader()
    .AllowAnyMethod()
);

app.UseAuthorization();

app.MapControllers();

app.Run();
