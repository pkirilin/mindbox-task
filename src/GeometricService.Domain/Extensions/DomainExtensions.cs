using GeometricService.Domain.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace GeometricService.Domain.Extensions
{
    public static class DomainExtensions
    {
        public static void AddFigureResolver(this IServiceCollection services, Action<IFigureResolverBuilder> configure)
        {
            services.AddScoped(sp =>
            {
                var builder = new FigureResolverBuilder();
                configure?.Invoke(builder);
                return builder.Build();
            });
        }
    }
}
