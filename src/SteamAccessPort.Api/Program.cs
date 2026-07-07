using Microsoft.EntityFrameworkCore;
using SteamAccessPort.Infrastructure.Data;
using SteamAccessPort.Application.Interfaces;
using SteamAccessPort.Infrastructure;
using SteamAccessPort.Infrastructure.Steam;
using SteamAccessPort.Application.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.Configure<SteamOptions>(builder.Configuration.GetSection("Steam"));

builder.Services.AddHttpClient();

builder.Services.AddDbContext<SteamGamesDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SteamGames")));

builder.Services.AddScoped<ISteamGameService, SteamApiGameService>();
builder.Services.AddScoped<IUserGameRepository, UserGameRepository>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.MapControllers();

app.Run();