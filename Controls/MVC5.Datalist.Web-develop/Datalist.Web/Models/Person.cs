using Datalist.Web.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace Datalist.Web.Models
{
    public class Person
    {
        [DatalistColumn(Hidden = true)]
        public Int32 Id { get; set; }

        [DatalistColumn]
        [Display(ResourceType = typeof(Titles), Name = "Name")]
        public String Name { get; set; }

        [DatalistColumn]
        [Display(ResourceType = typeof(Titles), Name = "Surname")]
        public String Surname { get; set; }

        [DatalistColumn]
        [Display(ResourceType = typeof(Titles), Name = "Income")]
        public Int32? Income { get; set; }

        [DatalistColumn(Format = "{0:d}")]
        [Display(ResourceType = typeof(Titles), Name = "Birthday")]
        public DateTime Birthday { get; set; }

        public Boolean? IsWorking { get; set; }
    }
}
