using WebApplication1.Services.Interfaces;

namespace WebApplication1.Services
{
    public class EnvironmentService : IEnvironmentService
    {
        public string GetEnvironmentVariable(string key)
        {
            return Environment.GetEnvironmentVariable(key);
        }
    }
}
