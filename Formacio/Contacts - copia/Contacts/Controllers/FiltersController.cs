using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Contacts.Controllers
{
    public class FiltersController : Controller
    {
        DBClases.clsDtaUtils dbUtils = new DBClases.clsDtaUtils();
        // GET: Filters
        public ActionResult Filters()
        {
            Models.FiltersModels aa = new Models.FiltersModels();
            aa.CityList = dbUtils.RetrieveContactCities("");
            aa.CountryList = dbUtils.RetrieveContactCountries("");
            return View("Filters" ,aa );
        }

               
        [HttpPost]
        public ActionResult Filters(Models.FiltersModels model)
        {
            try
            {
                // TODO: Add update logic here
                model.SelectionResult = "Chicks: " +
                                       string.Join(",", model.CityList.Where(item => item.IsSelected).Select(item => item.Caption));


                return View("Filters",model);
            }
            catch
            {
                return View();
            }
        }

      
    }
}
