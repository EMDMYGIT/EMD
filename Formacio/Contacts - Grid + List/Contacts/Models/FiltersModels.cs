using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Contacts.Models
{
    public class FiltersModels
    {

        public class CityFilter
        {
            public String CityName { get; set; }
        }
        public class CountryFilter
        {
            public String CountryName { get; set; }
        }

        //Properies to dropdown filters
        public IEnumerable<SelectListItem> CityList { get; set; }
        public String CityNameKey { get; set; }

        public IEnumerable<SelectListItem> CountriesList { get; set; }
        public int CountryNameKey { get; set; }

    }
}