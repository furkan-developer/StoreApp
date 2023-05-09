using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Filters
{
    public abstract class RequestParameterStateFilter : Attribute,IActionFilter, IOrderedFilter
    {
        public int Order { get; set; } = 0;
        public virtual void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public virtual void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}
