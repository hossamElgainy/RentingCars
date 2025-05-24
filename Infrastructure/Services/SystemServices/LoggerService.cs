using Core.Interfaces.IServices.SystemIServices;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace Infrastructure.Services.SystemServices
{
    // Infrastructure/Services/LoggerService.cs
    public class LoggerService : ILoggerService
    {
        private readonly ILogger _logger;

        public LoggerService(ILogger logger) // Note: Using Serilog.ILogger interface
        {
            _logger = logger;
        }

        public void LogInformation(string message) => _logger.Information(message);
        public void LogWarning(string message) => _logger.Warning(message);
        public void LogError(string message, Exception ex = null) => _logger.Error(ex, message);
        public void LogDebug(string message) => _logger.Debug(message);
    }
    public class SolvedLogEnricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            // Create the Solved property with default value false
            var solvedProperty = propertyFactory.CreateProperty("Solved", false);

            // Add or update the property in the log event
            logEvent.AddOrUpdateProperty(solvedProperty);
        }
    }

}



