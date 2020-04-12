using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vk_MVC.Authorize
{
    public class UserAuthorize : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.Request.Cookies.ContainsKey("Authorized"))
                context.Result = new RedirectToActionResult("Index", "Home", null);
            base.OnActionExecuting(context);
        }
    }
}
