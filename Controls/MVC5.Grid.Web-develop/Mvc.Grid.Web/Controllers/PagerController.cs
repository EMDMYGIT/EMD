using NonFactors.Mvc.Grid.Web.Context;
using System.Web.Mvc;

namespace NonFactors.Mvc.Grid.Web.Controllers
{
    public class PagerController : Controller
    {
        [HttpGet]
        public ActionResult SelectableRowsPerPage()
        {
            return View(PeopleRepository.GetPeople());
        }

        [HttpGet]
        public ActionResult PagesToDisplay()
        {
            return View(PeopleRepository.GetPeople());
        }

        [HttpGet]
        public ActionResult CurrentPage()
        {
            return View(PeopleRepository.GetPeople());
        }

        [HttpGet]
        public ActionResult RowsPerPage()
        {
            return View(PeopleRepository.GetPeople());
        }
    }
}
