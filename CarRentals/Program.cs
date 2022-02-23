using CarRentals.Models;
using CarRentals.Services;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using ZymLabs.NSwag.FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers()
    .AddFluentValidation(options =>
    {
        options.ImplicitlyValidateChildProperties = true;
        options.ImplicitlyValidateRootCollectionElements = true;
        options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());

    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocument((config, provider) =>
{
    config.PostProcess = document =>
    {
        document.Info.Version = "v1";
        document.Info.Title = "Car Rentals API";
        document.Info.Description = "An API for concepts practice";
    };
    var fluentValidationProcessor = provider.CreateScope().ServiceProvider.GetService<FluentValidationSchemaProcessor>();
    config.SchemaProcessors.Add(fluentValidationProcessor);
});
//Add in memory Database
builder.Services.AddDbContext<CarRentalDbContext>(options =>
{
    options.UseInMemoryDatabase("Cars");
});

builder.Services.AddScoped<ICarRentalDbContext>(provider => provider.GetService<CarRentalDbContext>());

builder.Services.AddScoped<ICarService, CarService>();

builder.Services.AddScoped<FluentValidationSchemaProcessor>();


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
