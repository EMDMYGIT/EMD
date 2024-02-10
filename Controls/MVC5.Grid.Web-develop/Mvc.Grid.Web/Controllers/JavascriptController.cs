using NonFactors.Mvc.Grid.Web.Context;
using System;
using System.Threading;
using System.Web.Mvc;

namespace NonFactors.Mvc.Grid.Web.Controllers
{
    public class JavascriptController : Controller
    {
        [HttpGet]
        public ActionResult RowClicked()
        {
            return View(PeopleRepository.GetPeople());
        }

        [HttpGet]
        public ActionResult Reload()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult ReloadGrid(String name)
        {
            return PartialView("_ReloadGrid", PeopleRepository.GetPeople(name));
        }

        [HttpGet]
        public ActionResult RequestType()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ReloadEvents()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ReloadEventsGrid()
        {
            Thread.Sleep(500);

            return PartialView("_ReloadGrid", PeopleRepository.GetPeople());
        }
    }
}
