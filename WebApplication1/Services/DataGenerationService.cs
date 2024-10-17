using System.Globalization;
using WebApplication1.Services.Interfaces;

namespace WebApplication1.Services
{
    public class DataGenerationService : IDataGenerationService
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly string _dateFormat;

        public DataGenerationService(IEnvironmentService environmentService)
        {
            _dateFormat = environmentService.GetEnvironmentVariable("DATE_FORMAT") ?? "yyyy-MM-dd";
        }

        public async Task<IEnumerable<WeatherForecast>> GetForecast(int start, int daysToForecast)
        {
            if (start <= 0)
            {
                throw new ArgumentException("Parametro `start`  debe de ser mayor a cero.");
            }

            if (daysToForecast <= 0)
            {
                throw new ArgumentException("Parametro `daysToForecast`  debe de ser mayor a cero.");
            }


            var result = Enumerable.Range(start, daysToForecast).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index).ToString(_dateFormat, CultureInfo.InvariantCulture),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();

            return await Task.FromResult(result);
        }
    }
}
