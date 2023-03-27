
using Microsoft.AspNetCore.Diagnostics;
using Services;
using WebAPI.ErrorModels;
using static System.Net.Mime.MediaTypeNames;

namespace WebAPI.Extensitions
{
    public static class CustomMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(
            this WebApplication app,
            ILoggerService logger) 
        {
            app.UseExceptionHandler(exceptionHandlerApp =>
            {
                exceptionHandlerApp.Run(async context =>
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = Application.Json;

                    var exceptionHandlerPathFeature =
                        context.Features.Get<IExceptionHandlerPathFeature>();

                    if (exceptionHandlerPathFeature is not null)
                    {
                        var error = exceptionHandlerPathFeature.Error;

                        logger.Error(error.Message); 

                        await context.Response.WriteAsync(new ErrorDetail("Internal Server Error", context.Response.StatusCode).ToString());
                    }
                });
            });
        }
    }
}
