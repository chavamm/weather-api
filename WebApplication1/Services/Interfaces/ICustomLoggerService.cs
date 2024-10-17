namespace WebApplication1.Services.Interfaces
{
    public interface ICustomLoggerService
    {
        void Info(string message);
        void Error(Object o);
    }
}
