namespace WebApplication1.Services.Interfaces
{
    public interface IDataGenerationService
    {
        Task<IEnumerable<WeatherForecast>> GetForecast(int start, int daysToForecast);
    }
}
