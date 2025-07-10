using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SalesWebMvc.Filters
{
    public class ProfileRequiredFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = context.RouteData.Values["controller"]?.ToString();
            var action = context.RouteData.Values["action"]?.ToString();

            if (controller == "Profile" && (action == "Select" || action == "Set"))
                return;

            var profile = context.HttpContext.Session.GetInt32("UserProfile");
            if (profile == null || profile == 0)
            {
                context.Result = new RedirectToActionResult("Select", "Profile", null);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
