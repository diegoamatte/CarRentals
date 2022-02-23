using CarRentals.Models;
using CarRentals.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CarRentalDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Cars")));

//builder.Services.AddScoped<CarRentalDbContext>(provider => provider.GetService<CarRentalDbContext>());

builder.Services.AddScoped<IService<Car>, CarService>();
builder.Services.AddScoped<IService<Client>, ClientService>();

builder.Services.AddTransient<IValidator<Car>, CarValidator>();

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
