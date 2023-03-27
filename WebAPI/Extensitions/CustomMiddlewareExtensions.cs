
using Microsoft.AspNetCore.Diagnostics;
using Services;
using Services.CustomExceptions;
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
                    context.Response.ContentType = Application.Json;

                    var exceptionHandlerPathFeature =
                        context.Features.Get<IExceptionHandlerPathFeature>();

                    if (exceptionHandlerPathFeature is not null)
                    {
                        var error = exceptionHandlerPathFeature.Error; 
                        logger.Error(error.Message);

                        context.Response.StatusCode = error switch
                        {
                            NotFoundException => StatusCodes.Status404NotFound,
                            _ => StatusCodes.Status500InternalServerError
                        };

                        if (context.Response.StatusCode is not StatusCodes.Status500InternalServerError)
                            await context.Response.WriteAsync(new ErrorDetail(
                                 error.Message,
                                 context.Response.StatusCode).ToString());

                        else
                            await context.Response.WriteAsync(new ErrorDetail(
                                "Internal Server Error",
                                context.Response.StatusCode).ToString());
                    }
                });
            });
        }
    }
}
