using System.Net;
using Weather;
using Microsoft.Extensions.DependencyInjection;
using Weather.Extensions;
using ILogger = Microsoft.Extensions.Logging.ILogger;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ILoggerr, ConsoleLogger>();
builder.Services.AddSingleton<IAuthorizationService, AuthorizationService>();
builder.Services.AddHttpClient<IWeatherService, WeatherService>();
builder.Services.AddTransient<ILoggingService, LoggingService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseLoggingMiddleware();
app.UseAuthorizationMiddleware();
app.UseWeatherMiddleware();

app.UseHttpsRedirection();
app.UseRouting();


app.MapControllers();

app.Run();


