using System;
using Microsoft.Extensions.Logging;

namespace Kulba.Services.CayenneService
{
    internal static class LoggerExtensions
    {
        private static Action<ILogger, DateTimeOffset, int, Exception> _programStarting;
        private static Action<ILogger, DateTimeOffset, Exception> _programStopping;

        static LoggerExtensions()
        {
            _programStarting = LoggerMessage.Define<DateTimeOffset, int>(LogLevel.Information, 1, "Starting at '{StartTime}' and 0x{Hello:X} is hex of 42");
            _programStopping = LoggerMessage.Define<DateTimeOffset>(LogLevel.Information, 2, "Stopping at '{StopTime}'");
        }
        public static void ProgramStarting(this ILogger logger, DateTimeOffset startTime, int hello, Exception exception = null)
        {
            _programStarting(logger, startTime, hello, exception);
        }
        public static void ProgramStopping(this ILogger logger, DateTimeOffset stopTime, Exception exception = null)
        {
            _programStopping(logger, stopTime, exception);
        }
    }
}