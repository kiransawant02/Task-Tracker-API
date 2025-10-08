using Task_Tracker_API.BLL.ApiMethods;
using Task_Tracker_API.DAL;

public static class DependencyInjection
{
    public static IServiceCollection AddProjectServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<Methods>();
        services.AddScoped<SQLLogic>();

        // Other services...
        return services;
    }
}
