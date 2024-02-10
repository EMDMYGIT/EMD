using Datalist.Web.Datalists;
using System.Web.Mvc;

namespace Datalist.Web.Controllers
{
    public class ColumnController : Controller
    {
        [HttpGet]
        public ViewResult Key()
        {
            return View();
        }

        [HttpGet]
        public ViewResult Header()
        {
            return View();
        }

        [HttpGet]
        public ViewResult Position()
        {
            return View();
        }

        [HttpGet]
        public ViewResult CssClass()
        {
            return View();
        }


        [HttpGet]
        public JsonResult AllPeople(DatalistFilter filter)
        {
            return Json(new PeopleDatalist { Filter = filter }.GetData(), JsonRequestBehavior.AllowGet);
        }
    }
}
