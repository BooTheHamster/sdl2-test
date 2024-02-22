using Serilog;
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

#if DEBUG
        var fullLogFilePath = Path.Combine("./", LogFileName);
#else
        string logFolderName;
        string logFolderFullPath;
        
        if (OperatingSystem.IsWindows)
        {
            logFolderFullPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            logFolderName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        }
        else
        {
            logFolderFullPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            logFolderName = $".{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}";
        }
        
        var fullLogFilePath = Path.Combine(logFolderFullPath, logFolderName ?? string.Empty, LogFileName);
#endif

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