using Datalist.Web.Datalists;
using System;
using System.Globalization;
using System.Web.Mvc;

namespace Datalist.Web.Controllers
{
    public class DatalistController : Controller
    {
        [HttpGet]
        public ViewResult Title()
        {
            return View();
        }

        [HttpGet]
        public ViewResult SourceUrl()
        {
            return View();
        }

        [HttpGet]
        public ViewResult Localization()
        {
            return View();
        }

        [HttpGet]
        public ViewResult HiddenData()
        {
            return View();
        }

        [HttpGet]
        public ViewResult Multi()
        {
            return View();
        }

        [HttpGet]
        public ViewResult AdditionalFilters()
        {
            return View();
        }

        [HttpGet]
        public ViewResult SortOptions()
        {
            return View();
        }

        [HttpGet]
        public ViewResult ColumnPosition()
        {
            return View();
        }

        [HttpGet]
        public ViewResult ColumnHeader()
        {
            return View();
        }

        [HttpGet]
        public ViewResult PagingOptions()
        {
            return View();
        }


        [HttpGet]
        public ViewResult Id()
        {
            return View();
        }

        [HttpGet]
        public ViewResult Autocomplete()
        {
            return View();
        }


        [HttpGet]
        public ViewResult Configuration()
        {
            return View();
        }


        [HttpGet]
        public JsonResult AllPeople(DatalistFilter filter, Int32? autocompleteIncome, Int32? datalistIncome)
        {
            filter.AdditionalFilters["Income"] = autocompleteIncome ?? datalistIncome;

            return Json(new PeopleDatalist { Filter = filter }.GetData(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult WorkingPeople(DatalistFilter filter)
        {
            filter.AdditionalFilters["IsWorking"] = true;

            return Json(new PeopleDatalist { Filter = filter }.GetData(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult LocalizedPeople(DatalistFilter filter)
        {
            CultureInfo.CurrentCulture = new CultureInfo("de");
            CultureInfo.CurrentUICulture = new CultureInfo("de");

            return Json(new PeopleDatalist { Filter = filter }.GetData(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult AllPeopleWithIncome(DatalistFilter filter, Int32? autocompleteIncome)
        {
            filter.AdditionalFilters["Income"] = autocompleteIncome;

            return Json(new PeopleDatalist { Filter = filter }.GetData(), JsonRequestBehavior.AllowGet);
        }
    }
}
