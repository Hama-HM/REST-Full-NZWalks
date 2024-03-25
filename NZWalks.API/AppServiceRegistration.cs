using Microsoft.Extensions.DependencyInjection;
using NZWalks.API.Repositories;
using NZWalks.API.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NZWalks.API;

public static class AppServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddScoped<IRegionRepository, SQLRegionRepository>();
        services.AddScoped<IWalkRepository, SQLWalkRepository>();
        services.AddScoped<ITokenRepository, TokenRepository>();
        return services;
    }
}
