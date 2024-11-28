using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;

namespace DEMO_CSDL.Controllers
{
    public class Session : Controller
    {
        protected int? UserId { get; set; }
        protected string? UserName { get; set; }
        protected string? Email { get; set; }

        public Session() { }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            UserId = HttpContext.Session.GetInt32("UserId");
            UserName = HttpContext.Session.GetString("UserName");
            Email = HttpContext.Session.GetString("Email");

            if (UserId == null)
            {
                context.Result = RedirectToAction("Login", "TaiKhoan");
                return;
            }

            ViewData["UserId"] = UserId.ToString();
            ViewData["UserName"] = UserName;
            ViewData["Email"] = Email;

            base.OnActionExecuting(context);
        }
    }
}
