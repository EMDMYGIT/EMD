using Datalist;
using MVC5Test.Components.Datalists;
using MVC5Test.Components.Mvc;
using MVC5Test.Components.Security;
using MVC5Test.Data.Core;
using MVC5Test.Objects;
using System;
using System.Web.Mvc;
using System.Web.SessionState;

namespace MVC5Test.Controllers
{
    [AllowUnauthorized]
    [SessionState(SessionStateBehavior.ReadOnly)]
    public class DatalistController : BaseController
    {
        private IUnitOfWork UnitOfWork { get; }

        public DatalistController(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        [NonAction]
        public virtual JsonResult GetData(MvcDatalist datalist, DatalistFilter filter)
        {
            datalist.Filter = filter;

            return Json(datalist.GetData(), JsonRequestBehavior.AllowGet);
        }

        [AjaxOnly]
        public JsonResult Role(DatalistFilter filter)
        {
            return GetData(new MvcDatalist<Role, RoleView>(UnitOfWork), filter);
        }

        protected override void Dispose(Boolean disposing)
        {
            UnitOfWork.Dispose();

            base.Dispose(disposing);
        }
    }
}
