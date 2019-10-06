using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Com.Melon.Wrap.Site.Core.Port.Adapter.Mvc
{
    public class MelonAuthorizationFilter: IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionDescriptor.EndpointMetadata.All(x => x.GetType() != typeof(AllowAnonymousAttribute)) && (context.HttpContext.User == null||!context.HttpContext.User.Claims.Any()))
            {
                context.Result = new RedirectToActionResult("Login", "Account", new { Area = "Identity" });
            }
        }
    }
}
