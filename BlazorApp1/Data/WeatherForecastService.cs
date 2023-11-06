using Microsoft.EntityFrameworkCore;

namespace BlazorApp1.Data
{
    public class WeatherForecastService
    {
        public WeatherForecastService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        private readonly ApplicationDbContext dbContext;

        public async Task<IEnumerable<WeatherForecast>> GetForecastAsync(DateOnly startDate)
        {
            var newForecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = startDate.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            }).ToArray();

            dbContext.WeatherForecasts.AddRange(newForecasts);
            await dbContext.SaveChangesAsync();

            return await dbContext.WeatherForecasts.ToListAsync();
        }
    }
}