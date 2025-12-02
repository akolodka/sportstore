namespace SportsStore.Extensions
{
    public static class ConfigurationExtensions
    {
        public static string? GetSportsStoreConnectionString(this IConfiguration configuration)
        {
            return configuration?.GetConnectionString("SportsStoreConnection");
        }
    }
}