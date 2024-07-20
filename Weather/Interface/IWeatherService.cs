namespace Weather;

public interface IWeatherService
{
    Task<string> GetWeatherAsync(string city);
}