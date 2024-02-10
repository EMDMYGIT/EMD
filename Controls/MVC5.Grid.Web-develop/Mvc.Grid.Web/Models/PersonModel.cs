using System;
using System.Collections.Generic;

namespace NonFactors.Mvc.Grid.Web.Models
{
    public class PersonModel
    {
        public Int32 Id { get; set; }
        public String Name { get; set; }
        public String Surname { get; set; }

        public Int32 Age { get; set; }
        public DateTime Birthday { get; set; }
        public Boolean? IsWorking { get; set; }

        public IList<PersonModel> Children { get; set; }
    }
}
