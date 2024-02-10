using MVC5Test.Components.Security;
using MVC5Test.Services;
using System.Web.Mvc;

namespace MVC5Test.Controllers
{
    [AllowUnauthorized]
    public class HomeController : ServicedController<IAccountService>
    {
        public HomeController(IAccountService service)
            : base(service)
        {
        }

        [HttpGet]
        public ActionResult Index()
        {
            if (!Service.IsActive(CurrentAccountId))
                return RedirectIfAuthorized("Logout", "Auth");

            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ViewResult Error()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ViewResult NotFound()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Unauthorized()
        {
            if (!Service.IsActive(CurrentAccountId))
                return RedirectIfAuthorized("Logout", "Auth");

            return View();
        }
    }
}
