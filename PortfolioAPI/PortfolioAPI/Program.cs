using Microsoft.EntityFrameworkCore;
using PortfolioAPI.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddControllers();
builder.Services.AddDbContext<APIDB>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("con")));

var app = builder.Build();
app.Run();
