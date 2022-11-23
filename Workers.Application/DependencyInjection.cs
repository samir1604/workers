using Microsoft.Extensions.DependencyInjection;
using Workers.Application.Services.Authentication;

namespace Workers.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection service)
    {
        service.AddScoped<IAuthenticationService, AuthenticationService>();
        return service;
    }
}