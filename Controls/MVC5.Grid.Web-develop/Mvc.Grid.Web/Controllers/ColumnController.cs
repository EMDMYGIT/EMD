using NonFactors.Mvc.Grid.Web.Context;
using System.Web.Mvc;

namespace NonFactors.Mvc.Grid.Web.Controllers
{
    public class ColumnController : Controller
    {
        [HttpGet]
        public ActionResult RenderingValue()
        {
            return View(PeopleRepository.GetPeople());
        }

        [HttpGet]
        public ActionResult MultiFiltering()
        {
            return View(PeopleRepository.GetPeople());
        }

        [HttpGet]
        public ActionResult Filtering()
        {
            return View(PeopleRepository.GetPeople());
        }

        [HttpGet]
        public ActionResult CustomFilter()
        {
            return View(PeopleRepository.GetPeople());
        }

        [HttpGet]
        public ActionResult InitialSort()
        {
            return View(PeopleRepository.GetPeople());
        }

        [HttpGet]
        public ActionResult FirstSort()
        {
            return View(PeopleRepository.GetPeople());
        }

        [HttpGet]
        public ActionResult Sorting()
        {
            return View(PeopleRepository.GetPeople());
        }

        [HttpGet]
        public ActionResult Encoding()
        {
            return View(PeopleRepository.GetPeople());
        }

        [HttpGet]
        public ActionResult Formatting()
        {
            return View(PeopleRepository.GetPeople());
        }

        [HttpGet]
        public ActionResult Styling()
        {
            return View(PeopleRepository.GetPeople());
        }

        [HttpGet]
        public ActionResult Title()
        {
            return View(PeopleRepository.GetPeople());
        }
    }
}