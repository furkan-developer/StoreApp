using Presentation.Filters;

namespace WebAPI.Extensitions
{
    public static class ActionFilterConfigurations
    {
        public static void ConfigureActionFilters(this IServiceCollection services)
        {
            services.AddSingleton(typeof(LogFilter));
            services.AddScoped(typeof(ValidationFilter));
        }
    }
}
