using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using MvcControls.Controls;
using System.Diagnostics;

namespace Contacts.Models
{
    public class FiltersModels
    {
        //Properies to dropdown filters
        public List<CityFilter> CityList { get; set; }
        public List<CountryFilter> CountryList { get; set; }


        public string SelectionResult { get; set; }
        //Grid filters
        public class CityFilterGrid
        {
            public String CityName { get; set; }
        }
        public class CountryFilterGrid
        {
            public String CountryName { get; set; }
        }
    }
    public class CityFilter : ISelectable
    {
        public int Id { get; set; }
        public String Caption { get; set; }
        public bool IsSelected { get; set; }

        public CityFilter()
        {

        }
        public CityFilter(String caption)
        {
            Caption = caption;
        }
    }
    public class CountryFilter : ISelectable
    {
        public int Id { get; set; }
        public String Caption { get; set; }
        public bool IsSelected { get; set; }

        public CountryFilter()
        {

        }
        public CountryFilter(String caption)
        {
            Caption = caption;
        }
    }
    
}