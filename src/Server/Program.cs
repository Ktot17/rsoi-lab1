using BLComponent;
using BLComponent.InputPorts;
using BLComponent.OutputPorts;
using DBComponent.Postgres;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<PostgresDbContext>(options =>
    options.UseNpgsql(builder.Configuration["ConnectionStrings:Postgres"]!));
builder.Services.AddScoped<IPersonRepository, PostgresPersonRepository>();

builder.Services.AddScoped<IPersonManager, PersonManager>();

var app = builder.Build();

app.MapControllers();
app.MapGet("api/v1/health", () => Results.Ok("Healthy"));

await app.RunAsync();