using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MvcWhatsUp.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Request.Cookies.TryGetValue("PreferredTheme", out string? theme))
            {
                ViewData["PreferredTheme"] = theme ?? "default";
            }
            else
            {
                ViewData["PreferredTheme"] = "default";
            }

            base.OnActionExecuting(context);
        }
    }
}
