using Datalist.Web.Datalists;
using System.Web.Mvc;

namespace Datalist.Web.Controllers
{
    public class JavascriptController : Controller
    {
        [HttpGet]
        public ViewResult Reload()
        {
            return View();
        }

        [HttpGet]
        public ViewResult Browse()
        {
            return View();
        }

        [HttpGet]
        public ViewResult Select()
        {
            return View();
        }

        [HttpGet]
        public ViewResult Readonly()
        {
            return View();
        }

        [HttpGet]
        public ViewResult FilterChange()
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
