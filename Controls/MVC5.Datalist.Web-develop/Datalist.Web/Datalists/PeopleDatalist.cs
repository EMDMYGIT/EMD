using Datalist.Web.Context;
using Datalist.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Datalist.Web.Datalists
{
    public class PeopleDatalist : MvcDatalist<Person>
    {
        public PeopleDatalist()
        {
            Filter.Rows = 5;
            Title = "People";
            Url = "AllPeople";
            Filter.Sort = "Income";
            Filter.Order = DatalistSortOrder.Desc;
        }

        public override IQueryable<Person> GetModels()
        {
            return PeopleRepository.Get();
        }

        public override void AddAutocomplete(Dictionary<String, String> row, Person model)
        {
            row[AcKey] = model.Name + " " + model.Surname;
        }
        public override void AddData(Dictionary<String, String> row, Person model)
        {
            base.AddData(row, model);

            if (model.IsWorking == true)
                row["IsWorking"] = "Person is employed";
            else if (model.IsWorking == false)
                row["IsWorking"] = "Person is unemployed";
            else
                row["IsWorking"] = "It's unknown is person is employed or not";
        }
    }
}
