using CarRentals.Models;
using CarRentals.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Rent a Car API",
        Description = "An API made for concepts practice",
    });
    
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

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
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
