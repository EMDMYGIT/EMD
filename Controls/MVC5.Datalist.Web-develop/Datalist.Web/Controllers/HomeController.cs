using Datalist.Web.Datalists;
using System.Web.Mvc;

namespace Datalist.Web.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ViewResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Installation()
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
