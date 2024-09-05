using Microsoft.EntityFrameworkCore;
using PlatformService.Data;
using PlatformService.Heplers;
using PlatformService.SyncDataServices.Http;

var builder = WebApplication.CreateBuilder(args);

// No need to specify URLs here; it will read from configuration.
builder.WebHost.ConfigureKestrel(options =>
{
    var url = builder.Configuration.GetSection("Kestrel");
    options.Configure(url);
});

// Explicitly configure Kestrel to use the settings from configuration, including URLs.
//builder.WebHost.UseKestrel();

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseInMemoryDatabase("inMem");
});

builder.Services.AddScoped<IPlatformRepo, PlatformRepo>();
builder.Services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();

builder.Services.AddControllers();

Utils.Write($"--------------------");
Utils.Write($"--> Command Service Endpoint {builder.Configuration["CommandService"]}");
Utils.Write($"--------------------");

//builder.WebHost.UseUrls("http://*:8001");

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());



var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

PrebDb.PrepPopulation(app);

app.Run();
