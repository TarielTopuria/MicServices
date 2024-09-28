namespace Ordering.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Database");

            // Add services to the contianer

            //services.AddDbContext<ApplicationDbContext>(opts =>
            //  opts.UseSqlServer(connectionString));

            return services;
        }
    }
}
