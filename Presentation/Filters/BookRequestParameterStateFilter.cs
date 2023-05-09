using Entities.RequestFeatures;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Presentation.Filters
{
    public class BookRequestParameterStateFilter : RequestParameterStateFilter
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            KeyValuePair<string, object?> arg = context.ActionArguments.ToList().FirstOrDefault(a => a.Value.GetType().BaseType.Name.Equals(nameof(RequestParameters)));

            if (arg.Value != null)
            {
                var bookRequestParameter = (BookRequestParameters)arg.Value;
                CheckPriceRange(context,bookRequestParameter.MaxPrice, bookRequestParameter.MinPrice);
                
            }
        }

        private void CheckPriceRange(ActionExecutingContext context, uint maxPrice,uint minPrice)
        {
            if (maxPrice < minPrice)
                context.ModelState.AddModelError("Price Range Condition", "Should be Max. Price > Min Price");
        }
    }
}
