using Microsoft.EntityFrameworkCore;
using MovieCatalogAPI.Data;
using MovieCatalogAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("MovieCatelogDb"));
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
 

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (!context.Directors.Any())
    {
        var director1 = new Director { Id = 1, Name = "Christopher Nolan" };
        var director2 = new Director { Id = 2, Name = "Rajkumar Hirani" };

        context.Directors.AddRange(director1, director2);

        context.Movies.AddRange(
            new Movie { Id = 1, Title = "Inception", ReleaseYear = 2010, DirectorId = 1 },
            new Movie { Id = 2, Title = "3 Idiots", ReleaseYear = 2009, DirectorId = 2 }
        );

        context.SaveChanges();
    }
}

app.Run();
