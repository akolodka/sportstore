namespace SportsStore.Extensions
{
    public static class ConfigurationExtensions
    {
        public static string? GetMainConnectionString(this IConfiguration configuration)
        {
            return configuration?.GetConnectionString("SportsStoreConnection");
        }
    }
}