using CarRentals.Models;
using CarRentals.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocument();
//Add in memory Database
builder.Services.AddDbContext<CarRentalDbContext>(options =>
{
    options.UseInMemoryDatabase("Cars");
});

builder.Services.AddScoped<ICarRentalDbContext>(provider => provider.GetService<CarRentalDbContext>());

builder.Services.AddScoped<ICarService, CarService>();

builder.Services.AddTransient<IValidator<Car>, CarValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi3();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
