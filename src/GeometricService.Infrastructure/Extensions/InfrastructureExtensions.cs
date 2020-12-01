using GeometricService.Domain.Repositories;
using GeometricService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GeometricService.Infrastructure.Extensions
{
    public static class InfrastructureExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<GeometricServiceContext>(options =>
            {
                options.UseSqlite(connectionString);
            });

            services.AddScoped<IFiguresRepository, FiguresRepository>();
        }
    }
}
