using Serilog;
using System;
using System.IO;

namespace Sdl2Test.Services;

public static class LoggerFactory
{
    private const string LogFileName = "log.txt";
    private const int LogFileSizeInBytes = 10 * 1024 * 1024;
    private const string LogFormat = "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}";

    private static Serilog.Core.Logger _logger;

    public static ILogger GetLogger()
    {
        if (_logger != null)
        {
            return _logger;
        }

        var logFolderName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        var fullLogFilePath = "";

        if (OperatingSystem.IsWindows)
        {
            var userAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            fullLogFilePath = Path.Combine(userAppDataPath, logFolderName ?? string.Empty, LogFileName);
        }

        var configuration = new LoggerConfiguration()
            .WriteTo
            .File(
                fullLogFilePath,
                outputTemplate: LogFormat,
                fileSizeLimitBytes: LogFileSizeInBytes,
                rollOnFileSizeLimit: true);

        if (OperatingSystem.IsWindows)
        {
            // В Windows параллельно логгируем в отладочную консоль.
            configuration = configuration
                .MinimumLevel.Debug()
                .WriteTo
                .Debug(outputTemplate: LogFormat);
        }

        _logger = configuration.CreateLogger();
        return _logger;
    }
}