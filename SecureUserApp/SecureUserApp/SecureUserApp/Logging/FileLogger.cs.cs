using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureUserApp.Logging
{
    public static class FileLogger
    {
        private const string LogFile = "log.txt";

        // For normal messages (success operations)
        public static void LogInfo(string message)
        {
            File.AppendAllText(LogFile,
                $"{DateTime.Now:u} [INFO] {message}{Environment.NewLine}");
        }

        // For errors (important requirement in assignment)
        public static void LogError(Exception ex, string message)
        {
            File.AppendAllText(LogFile,
                $"{DateTime.Now:u} [ERROR] {message}{Environment.NewLine}" +
                $"Exception: {ex.Message}{Environment.NewLine}" +
                $"StackTrace: {ex.StackTrace}{Environment.NewLine}" +
                $"----------------------------------------{Environment.NewLine}");
        }
    }
}
