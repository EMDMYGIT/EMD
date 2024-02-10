using MVC5Test.Controllers;
using MVC5Test.Services;
using MVC5Test.Validators;
using System.Web.Mvc;

namespace MVC5Test.Tests.Unit.Controllers
{
    public class ValidatedControllerProxy : ValidatedController<IValidator, IService>
    {
        protected ValidatedControllerProxy(IValidator validator, IService service)
            : base(validator, service)
        {
        }

        public void BaseOnActionExecuting(ActionExecutingContext context)
        {
            OnActionExecuting(context);
        }
    }
}
