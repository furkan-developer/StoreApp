using Entities.LogModel;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Filters
{
    public class LogFilter : IActionFilter
    {
        private readonly ILoggerService _loggerService;

        public LogFilter(ILoggerService logger)
        {
            _loggerService = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _loggerService.Info(Log("OnActionExecuting", context.RouteData));
        }

        private string Log(string modelName, RouteData routeData)
        {
            var logDetails = new LogDetails()
            {
                ModelModel = modelName,
                Controller = routeData.Values["controller"],
                Action = routeData.Values["action"]
            };

            if (routeData.Values.Count >= 3)
                logDetails.Id = routeData.Values["Id"];

            return logDetails.ToString();
        }
    }
}
