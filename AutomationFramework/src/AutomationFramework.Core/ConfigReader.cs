using Microsoft.Extensions.Configuration;
using System.IO;

namespace AutomationFramework.Core
{
    public static class ConfigReader
    {
        private static IConfigurationRoot Configuration { get; }

        static ConfigReader()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            Configuration = builder.Build();
        }

        public static string GetBaseUrl()
        {
            return Configuration["ApiSettings:BaseUrl"];
        }

        public static int GetTimeout()
        {
            return int.Parse(Configuration["ApiSettings:Timeout"]);
        }
    }
}