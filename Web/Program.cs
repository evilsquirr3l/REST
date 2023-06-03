using System.Text.Json.Serialization;
using Application.Interfaces;
using Application.Mapper;
using Application.Services;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var isDevelopment = builder.Environment.IsDevelopment();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContextPool<CatalogDbContext>(options =>
{
    options.UseInMemoryDatabase("CatalogDb");
    options.EnableDetailedErrors(isDevelopment);
    options.EnableSensitiveDataLogging(isDevelopment);
}).AddScoped<ICatalogDbContext>(provider => provider.GetService<CatalogDbContext>()!);

builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<IItemService, ItemService>();
builder.Services.AddAutoMapper(typeof(MapperProfile));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
