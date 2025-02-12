using Serilog;
using Serilog.Events;
using System;

namespace AutomationFramework.Core
{
    public static class Logger
    {
        private static readonly ILogger _logger;

        static Logger()
        {
            _logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        public static void Debug(string message) => _logger.Debug(message);
        public static void Information(string message) => _logger.Information(message);
        public static void Warning(string message) => _logger.Warning(message);
        public static void Error(string message) => _logger.Error(message);
        public static void Fatal(string message) => _logger.Fatal(message);
    }
}