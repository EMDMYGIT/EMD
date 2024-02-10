using MVC5Test.Controllers;
using MVC5Test.Services;
using System.Web.Mvc;

namespace MVC5Test.Tests.Unit.Controllers
{
    public class ServicedControllerProxy : ServicedController<IService>
    {
        public ServicedControllerProxy(IService service)
            : base(service)
        {
        }

        public void BaseOnActionExecuting(ActionExecutingContext context)
        {
            OnActionExecuting(context);
        }
    }
}
