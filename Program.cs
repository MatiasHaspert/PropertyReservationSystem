using Microsoft.EntityFrameworkCore;
using PropertyReservation.Domain.Interfaces;
using PropertyReservation.Infrastructure.Data;
using PropertyReservation.Infrastructure.Repositories;
using PropertyReservation.Application.Interfaces;
using PropertyReservation.Application.Services;


var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
 
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString)
    );


// Inject dependencies
builder.Services.AddScoped<IPropertyImageRepository, PropertyImageRepository>();
builder.Services.AddScoped<IPropertyImageService, PropertyImageService>();
builder.Services.AddScoped<IPropertyAvailabilityRespository, PropertyAvailabilityRepository>();
builder.Services.AddScoped<IPropertyAvailabilityService, PropertyAvailabilityService>();
builder.Services.AddScoped<IAmenityRepository, AmenityRespository>();
builder.Services.AddScoped<IAmenityService, AmenityService>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();
builder.Services.AddScoped<IPropertyService, PropertyService>();
builder.Services.AddControllers();

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

app.UseStaticFiles(); // Habilitar el servir archivos estaticos desde wwwroot

app.Run();
