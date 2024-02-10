using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Contacts.Controllers
{
    public class DataListController : Controller
    {
        // GET: DataList
        public ActionResult DataListFilter()
        {
            DBClases.CityDatalist getdata = new DBClases.CityDatalist();
            
            return View("DataListFilter",getdata.GetModels());
        }
        [HttpGet]
        public JsonResult AllCities(Datalist.DatalistFilter filter)
        {
            DBClases.CityDatalist getdata = new DBClases.CityDatalist();
            getdata.Filter = filter;

            return Json(getdata.GetData());
        }
    }
}
