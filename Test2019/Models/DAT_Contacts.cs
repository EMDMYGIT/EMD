using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Test2019.Models
{

    public class TB_Contact
    {

        //[ID], [Contact], [Name], [Adress], [TelNo], [City], [CountryID], [CreateDate], [CreateUser]

        public int ID { get; set; }
        public string Contact { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Adress { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string TelNo { get; set; }
        [DataType(DataType.Text)]
        public String City { get; set; }
        [DataType(DataType.Text)]
        public int CountryID { get; set; }

        public DateTime CreateDate { get; set; }
        public string CreateUser { get; set; }

    }
}