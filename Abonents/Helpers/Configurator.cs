

using Microsoft.Extensions.Configuration;

namespace Abonents.Helpers
{
    public static class Configurator
    {
        public static string ConfigureApp(this IConfiguration configuration)
        {
            return configuration.GetValue<string>("ConnectionString");
        }
    }
}
