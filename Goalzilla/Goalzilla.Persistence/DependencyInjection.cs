using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Goalzilla.Goalzilla.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<GoalzillaDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("Goalzilla"));
                options.EnableSensitiveDataLogging();
            });
            return services;
        }
    }
}