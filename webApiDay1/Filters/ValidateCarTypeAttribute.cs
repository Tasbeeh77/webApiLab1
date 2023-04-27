using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using webApiDay1.Models;

namespace webApiDay1.Filters
{
    public class ValidateCarTypeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Car? car = context.ActionArguments["car"] as Car;

            var allowedTypes = "“Electric|Gas|Diesel|Hybrid".Split("|");

            if (car is null || !allowedTypes.Contains(car.Type))
            {
                context.ModelState.AddModelError("Type", "Type is not allowed");
                context.Result = new BadRequestObjectResult(context.ModelState);
        }
    }
}
}
