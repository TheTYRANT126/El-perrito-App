using System;
using System.IO;
using System.Threading;

namespace ElPerrito.Core.Logging
{
    /// <summary>
    /// Implementación del patrón Singleton para logging
    /// Thread-safe usando lock
    /// </summary>
    public sealed class Logger
    {
        private static Logger? _instance;
        private static readonly object _lock = new object();
        private readonly string _logFilePath;
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        private Logger()
        {
            string logDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");

            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }

            _logFilePath = Path.Combine(logDirectory, $"log_{DateTime.Now:yyyyMMdd}.txt");
        }

        public static Logger Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new Logger();
                        }
                    }
                }
                return _instance;
            }
        }

        public void LogInfo(string message)
        {
            Log("INFO", message);
        }

        public void LogWarning(string message)
        {
            Log("WARNING", message);
        }

        public void LogError(string message, Exception? exception = null)
        {
            string fullMessage = exception != null
                ? $"{message}\nException: {exception.Message}\nStackTrace: {exception.StackTrace}"
                : message;
            Log("ERROR", fullMessage);
        }

        public void LogDebug(string message)
        {
#if DEBUG
            Log("DEBUG", message);
#endif
        }

        private void Log(string level, string message)
        {
            _semaphore.Wait();
            try
            {
                string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level}] {message}";
                File.AppendAllText(_logFilePath, logEntry + Environment.NewLine);

                // También escribir en consola para debugging
                Console.WriteLine(logEntry);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al escribir en log: {ex.Message}");
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task LogInfoAsync(string message)
        {
            await LogAsync("INFO", message);
        }

        public async Task LogErrorAsync(string message, Exception? exception = null)
        {
            string fullMessage = exception != null
                ? $"{message}\nException: {exception.Message}\nStackTrace: {exception.StackTrace}"
                : message;
            await LogAsync("ERROR", fullMessage);
        }

        private async Task LogAsync(string level, string message)
        {
            await _semaphore.WaitAsync();
            try
            {
                string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level}] {message}";
                await File.AppendAllTextAsync(_logFilePath, logEntry + Environment.NewLine);
                Console.WriteLine(logEntry);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al escribir en log: {ex.Message}");
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public string GetLogFilePath() => _logFilePath;
    }
}
