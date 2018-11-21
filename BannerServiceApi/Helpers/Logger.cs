using System;

namespace BannerServiceApi.Helpers
{
    public class Logger
    {
        /// <summary>
        /// Class abstracting the logging functionality
        /// </summary>
        public Logger()
        {
        }

        /// <summary>
        /// Log functionality
        /// </summary>
        /// <param name="logMessage"></param>
        public void Log(string logMessage)
        {
            //TODO: Replace with logging framework like serilog
            Console.WriteLine($"{DateTime.Now:s}: {logMessage}");
        }
    }
}