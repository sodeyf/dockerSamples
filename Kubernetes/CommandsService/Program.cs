var builder = WebApplication.CreateBuilder(args);

// No need to specify URLs here; it will read from configuration.
builder.WebHost.ConfigureKestrel(options =>
{
    var url = builder.Configuration.GetSection("Kestrel");
    options.Configure(url);
});

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
