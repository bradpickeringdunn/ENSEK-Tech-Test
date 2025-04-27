
using ENSEK;

var builder = WebApplication.CreateBuilder(args);

// Bind ApiSettings and add it to the services collection
builder.Configuration.AddJsonFile("appsettings.json", optional:false, reloadOnChange:true);

var startup = new StartUp(builder.Configuration);
startup.ConfigureServices(builder.Services);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
