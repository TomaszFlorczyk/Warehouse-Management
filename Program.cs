using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WarehouseMenagementAPI.Models;
using WarehouseMenagementAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var provider = builder.Services.BuildServiceProvider();
var configuration = provider.GetRequiredService<IConfiguration>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<WarehouseDbContext>(
    options => options.UseSqlServer(configuration.GetConnectionString("WarehouseDBConnection")));
builder.Services.AddScoped<WarehouseService>();
builder.Services.AddScoped<AlleyService>();

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
