using Microsoft.Extensions.Configuration;
using System.IO;

namespace DataAccessLayer
{
    public static class ConfigHelper
    {
        private static readonly IConfiguration _configuration;

        static ConfigHelper()
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory) 
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }

        public static string GetConnectionString()
        {
            return _configuration.GetConnectionString("MyDbConnection");
        }
    }
}
