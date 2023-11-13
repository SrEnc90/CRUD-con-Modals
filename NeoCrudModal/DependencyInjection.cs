using Microsoft.EntityFrameworkCore;
using NeoCrudModal.Context;

namespace NeoCrudModal
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDBContext>(options => { 
                options.UseSqlServer(configuration.GetConnectionString("conexionSQL")); 
            });

            return services;
        }
    }
}
