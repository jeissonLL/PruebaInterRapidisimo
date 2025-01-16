using MediatR;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;

namespace Presentation.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Registrar AutoMapper y sus perfiles
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // Registrar MediatR
            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
