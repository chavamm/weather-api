using System.Text.Json;
using WebApplication1.Services.Interfaces;

namespace WebApplication1.Services
{
    public class CustomLoggerService : ICustomLoggerService
    {
        private readonly ILogger<CustomLoggerService> _logger;
        public CustomLoggerService(ILogger<CustomLoggerService> logger) 
        {
            _logger = logger;
        }

        public void Error(Object o)
        {
            var jsonResponse = JsonSerializer.Serialize(o, new JsonSerializerOptions { WriteIndented = true });

            _logger.LogError("Error en la operacion. {jsonResponse}", jsonResponse);
        }

        public void Info(string message)
        {
            _logger.LogInformation(message);
        }
    }
}
