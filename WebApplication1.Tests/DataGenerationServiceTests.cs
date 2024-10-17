using Moq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Services;
using WebApplication1.Services.Interfaces;

namespace WebApplication1.Tests
{
    public class DataGenerationServiceTests
    {

        [Fact]
        public async Task GetForecast_ShouldReturnForecast_WhenDaysToForecastIsValidAsync()
        {
            // Arrange
            var mockService = new Mock<IDataGenerationService>();

            mockService.Setup(s => s.GetForecast(It.IsAny<int>(), It.IsAny<int>()))
                       .ReturnsAsync(new List<WeatherForecast>
                       {
                       new WeatherForecast { Date = "2024-01-01", TemperatureC = 20, Summary = "Warm" }
                       });

            // Act
            var result = await mockService.Object.GetForecast(1, 5);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("2024-01-01", result.First().Date);
        }


        [Fact]
        public async Task GetForecast_ShouldReturnCorrectNumberOfForecasts()
        {
            // Arrange
            var mockEnvironmentService = new Mock<IEnvironmentService>();
            mockEnvironmentService.Setup(s => s.GetEnvironmentVariable("DATE_FORMAT")).Returns("dd-MM-yyyy");

            var service = new DataGenerationService(mockEnvironmentService.Object);

            int start = 1;
            int daysToForecast = 10;

            // Act
            var result = await service.GetForecast(start, daysToForecast);

            // Assert
            Assert.NotEmpty(result);
            Assert.Equal(daysToForecast, result.Count());
        }

        [Fact]
        public async Task GetForecast_ShouldReturnCorrectForecastAndSetDateFormat()
        {
            // Arrange
            var mockEnvironmentService = new Mock<IEnvironmentService>();
            mockEnvironmentService.Setup(s => s.GetEnvironmentVariable("DATE_FORMAT")).Returns("dd-MM-yyyy");

            var service = new DataGenerationService(mockEnvironmentService.Object);

            int start = 1;
            int daysToForecast = 3;

            // Act
            var result = await service.GetForecast(start, daysToForecast);

            // Assert
            Assert.NotEmpty(result);
            Assert.Equal(daysToForecast, result.Count());

            // Validar que las fechas estan en el formato correcto
            foreach (var forecast in result)
            {
                DateTime parsedDate;
                bool isValidDate = DateTime.TryParseExact(forecast.Date, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate);
                Assert.True(isValidDate, $"La fecha '{forecast.Date}' no está en el formato esperado.");
            }
        }

        [Fact]
        public async Task GetForecast_ShouldThrowArgumentException_WhenDaysTtoForecast_IsInvalid()
        {
            // Arrange
            var mockEnvironmentService = new Mock<IEnvironmentService>();
            mockEnvironmentService.Setup(s => s.GetEnvironmentVariable("DATE_FORMAT")).Returns("dd-MM-yyyy");

            var service = new DataGenerationService(mockEnvironmentService.Object);

            int start = 1;
            int daysToForecast = -1;

            // Act
            // var result = await _dataGenerationService.GetForecast(1, daysToForecast);

            // Act, then, Assert
            await Assert.ThrowsAsync<ArgumentException>(() => service.GetForecast(1, daysToForecast));
            // Assert.Equal(daysToForecast, result.Count());
        }

        [Fact]
        public async Task GetForecast_ShouldThrowArgumentException_WhenStartDate_IsInvalid()
        {
            // Arrange
            var mockEnvironmentService = new Mock<IEnvironmentService>();
            mockEnvironmentService.Setup(s => s.GetEnvironmentVariable("DATE_FORMAT")).Returns("dd-MM-yyyy");

            var service = new DataGenerationService(mockEnvironmentService.Object);

            int start = 0;
            int daysToForecast = 10;

            // Act, then Assert
            await Assert.ThrowsAsync<ArgumentException>(() => service.GetForecast(start, daysToForecast));

        }
    }
}
