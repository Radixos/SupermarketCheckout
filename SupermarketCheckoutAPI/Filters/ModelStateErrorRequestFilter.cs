using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SupermarketCheckoutAPI.Filters
{
    internal class ModelStateErrorRequestFilterAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(
                    context.ModelState.ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors
                            .Select(e => e.ErrorMessage)
                            .ToArray()));
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}