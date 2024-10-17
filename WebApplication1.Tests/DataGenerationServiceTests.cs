using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Services;

namespace WebApplication1.Tests
{
    public class DataGenerationServiceTests
    {
        private readonly DataGenerationService _dataGenerationService;
        public DataGenerationServiceTests()
        {
            _dataGenerationService = new DataGenerationService();
        }

        [Fact]
        public async Task GetForecast_ShouldReturnCorrectNumberOfForecasts()
        {
            // Arrange
            int daysToForecast = 10;

            // Act
            var result = await _dataGenerationService.GetForecast(11, daysToForecast);

            // Assert
            Assert.NotEmpty(result);
            Assert.Equal(daysToForecast, result.Count());
        }

        [Fact]
        public async Task GetForecast_ShouldThrowArgumentException_WhenDaysTtoForecast_IsInvalid()
        {
            // Arrange
            int daysToForecast = -1;

            // Act
            // var result = await _dataGenerationService.GetForecast(1, daysToForecast);

            // Act, then, Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _dataGenerationService.GetForecast(1, daysToForecast));
            // Assert.Equal(daysToForecast, result.Count());
        }

        [Fact]
        public async Task GetForecast_ShouldThrowArgumentException_WhenStartDate_IsInvalid()
        {
            // Arrange
            int start = 0;
            int daysToForecast = 10;

            // Act, then Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _dataGenerationService.GetForecast(start, daysToForecast));

        }
    }
}
