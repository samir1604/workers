using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Workers.Application.Common.Interfaces.Authentication;
using Workers.Application.Common.Interfaces.Repositories;
using Workers.Application.Common.Interfaces.Services;
using Workers.Infrastructure.Authentication;
using Workers.Infrastructure.Persistance;
using Workers.Infrastructure.Services;

namespace Workers.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection service, ConfigurationManager configuration)
    {
        service.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        service.AddSingleton<IJwtGenerator, JwtGenerator>();
        service.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        service.AddScoped<IUserRepository, UserRepository>();
        return service;
    }
}
