using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication1.Models;
using WebApplication1.Services.Interfaces;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ICustomLoggerService _logger;
        private readonly IDataGenerationService _dataGenerationService;

        public WeatherForecastController(ICustomLoggerService logger, IDataGenerationService dataGenerationService)
        {
            _logger = logger;
            _dataGenerationService = dataGenerationService;
        }

        [Authorize]
        [HttpGet("{daysToForecast:int}")]
        public async Task<ActionResult<IEnumerable<WeatherForecast>>> Get(int daysToForecast)
        {

            ResponseModel<IEnumerable<WeatherForecast>> response = new ResponseModel<IEnumerable<WeatherForecast>>();

            if (daysToForecast <= 0)
            {
                response.Status = false;
                response.Message = "Failed to request Get WeatherForecast";
                response.Errors.Add("El parametro <daysToForecast> debe de ser un entero mayor a cero.");

                _logger.Error(response);

                return BadRequest(response);
            }

            var result =  await _dataGenerationService.GetForecast(1, daysToForecast);

            response.Status = true;
            response.Message = "Successfully requested";
            response.Data = result;

            _logger.Info($"Status: {response.Status} - {response.Message}");

            return Ok(response);
        }
    }
}
