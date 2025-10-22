using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using lab1mvc.Models;

namespace lab1mvc.Filters
{
    public class ValidateLocationAttribute : ActionFilterAttribute
    {
        private readonly string[] Locations = new[] { "USA", "Egypt" };

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // Try to get Department model from action arguments
            if (context.ActionArguments.TryGetValue("department", out var value) && value is Department dept)
            {
                // Check if Location is valid
                if (string.IsNullOrWhiteSpace(dept.Location) ||
                    !Locations.Contains(dept.Location, StringComparer.OrdinalIgnoreCase))
                {
                    // Add error to ModelState
                    if (context.Controller is Controller controller)
                    {
                        controller.ModelState.AddModelError("Location", $"Location must be either 'USA' or 'Egypt'.");

                        // Return same view with the same model to show error
                        context.Result = controller.View(dept);
                        return;
                    }
                }
            }

            base.OnActionExecuting(context);
        }
    }
}

