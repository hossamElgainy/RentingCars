using Core.Dtos.Response;

namespace Core.Interfaces.IServices.SystemIServices
{
    public interface ILoggerService
    {
        void LogInformation(string message);
        void LogWarning(string message);
        void LogError(string message, Exception ex = null);
        void LogDebug(string message);
    }
}
