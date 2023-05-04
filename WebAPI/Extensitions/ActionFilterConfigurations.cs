using Presentation.Filters;

namespace WebAPI.Extensitions
{
    public static class ActionFilterConfigurations
    {
        public static void ConfigureLogFilter(this IServiceCollection services)
        {
            services.AddSingleton(typeof(LogFilter));
        }
    }
}
