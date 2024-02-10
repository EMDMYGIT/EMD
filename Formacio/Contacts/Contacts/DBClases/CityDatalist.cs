using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DBClases;
using Datalist;
using Contacts;

namespace DBClases
{
    public class CityDatalist:Datalist.MvcDatalist<Contacts.Models.FiltersModels.CityFilterGrid>
    {
        private clsDtaUtils dtaUtils;

        public CityDatalist()
        {
            dtaUtils = new clsDtaUtils();
            Url = "AllCities";
            Title = "City";
            Multi = true;
            Filter.Rows = 5;
            Filter.Sort = "CityName";
            Filter.Order = DatalistSortOrder.Desc;
        }
        public override void AddAutocomplete(Dictionary<String, String> row, Contacts.Models.FiltersModels.CityFilterGrid model)
        {
            row[AcKey] = model.CityName;
        }
        public override IQueryable<Contacts.Models.FiltersModels.CityFilterGrid> GetModels()
        {
            return  dtaUtils.RetrieveContactCitiesGrid("").AsQueryable();

        }
    }
}


