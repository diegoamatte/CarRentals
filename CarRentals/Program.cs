using CarRentals.Models;
using CarRentals.Services;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Microsoft.AspNetCore.Http.Json;
using System.Text.Json.Serialization;
using System.Text.Json;
using CarRentals.Repositories;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddFluentValidation(options =>
    {
        options.ImplicitlyValidateChildProperties = true;
        options.ImplicitlyValidateRootCollectionElements = true;
        options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    })
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

//builder.Services.Configure<JsonOptions>(options => 
//    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter()));

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

builder.Services.AddFluentValidationRulesToSwagger();

builder.Services.AddDbContext<CarRentalDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Cars")));
builder.Services.AddScoped<IService<Car>, CarService>();
builder.Services.AddScoped<IService<Client>, ClientService>();
builder.Services.AddScoped<IService<Rental>, RentalService>();
builder.Services.AddScoped<INotificationService<Mail>, MailService>();
builder.Services.AddScoped<INotificationService<PrivateMessage>, TelegramService>();

builder.Services.AddScoped<IRepository<Car>, CarRepository>();
builder.Services.AddScoped<IRepository<Client>, ClientRepository>();
builder.Services.AddScoped<IRepository<Rental>, RentalRepository>();

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());


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

public partial class Program { };