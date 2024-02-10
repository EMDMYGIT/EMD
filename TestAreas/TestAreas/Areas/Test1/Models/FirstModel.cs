using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestAreas.Areas.Test1.Models
{
    public class FirstModel
    {
        public String Name { get; set; }
        public String Surname { get; set; }
        public Int32 Age { get; set; }
        public DateTime Birthday { get; set; }
        public bool Married { get; set; }
    }
    public class DataLoad
    {
        public List<FirstModel> Persons()
        {


            List<TestAreas.Areas.Test1.Models.FirstModel> returnFirst = new List<FirstModel>
            {   new FirstModel() { Name = "Name1", Surname = "Surname1", Age=1,Birthday=DateTime.Parse("01/01/1981"),Married=false },
                new FirstModel { Name = "Name2", Surname = "Surname2" , Age=2,Birthday=DateTime.Parse("01/01/1982") ,Married=false},
                new FirstModel { Name = "Name3", Surname = "Surname3" , Age=3,Birthday=DateTime.Parse("01/01/1983"),Married=false},
                new FirstModel { Name = "Name4", Surname = "Surname4" , Age=4,Birthday=DateTime.Parse("01/01/1984") ,Married=false},
                new FirstModel { Name = "Name5", Surname = "Surname5" , Age=5,Birthday=DateTime.Parse("01/01/1985"),Married=false},
                new FirstModel { Name = "Name6", Surname = "Surname6" , Age=6,Birthday=DateTime.Parse("01/01/1986"),Married=false},
                new FirstModel { Name = "Name7", Surname = "Surname7" , Age=7,Birthday=DateTime.Parse("01/01/1987") ,Married=false},
                new FirstModel { Name = "Name8", Surname = "Surname8" , Age=8,Birthday=DateTime.Parse("01/01/1988") ,Married=false},
                new FirstModel { Name = "Name9", Surname = "Surname9" , Age=9,Birthday=DateTime.Parse("01/01/1989") ,Married=false}
                };
            returnFirst.Add(new FirstModel { Name = "Name10", Surname = "Surname10", Age = 10, Birthday = DateTime.Parse("01/01/1990"), Married = true });
            returnFirst.Add(new FirstModel { Name = "Name11", Surname = "Surname11", Age = 11, Birthday = DateTime.Parse("01/01/1991"), Married = true });
            returnFirst.Add(new FirstModel { Name = "Name12", Surname = "Surname12", Age = 12, Birthday = DateTime.Parse("01/01/1992"), Married = true });
            returnFirst.Add(new FirstModel { Name = "Name13", Surname = "Surname13", Age = 10, Birthday = DateTime.Parse("01/01/1993"), Married = true });
            returnFirst.Add(new FirstModel { Name = "Name14", Surname = "Surname14", Age = 13, Birthday = DateTime.Parse("01/01/1994"), Married = true });
            returnFirst.Add(new FirstModel { Name = "Name15", Surname = "Surname15", Age = 14, Birthday = DateTime.Parse("01/01/1995"), Married = true });
            returnFirst.Add(new FirstModel { Name = "Name16", Surname = "Surname16", Age = 15, Birthday = DateTime.Parse("01/01/1996"), Married = true });
            returnFirst.Add(new FirstModel { Name = "Name17", Surname = "Surname17", Age = 16, Birthday = DateTime.Parse("01/01/1997"), Married = true });
            returnFirst.Add(new FirstModel { Name = "Name18", Surname = "Surname18", Age = 17, Birthday = DateTime.Parse("01/01/1998"), Married = true });
            returnFirst.Add(new FirstModel { Name = "Name19", Surname = "Surname19", Age = 18, Birthday = DateTime.Parse("01/01/1999"), Married = true });
            returnFirst.Add(new FirstModel { Name = "Name20", Surname = "Surname20", Age = 19, Birthday = DateTime.Parse("01/01/2000"), Married = true });

            returnFirst.Add(new FirstModel { Name = "Name20", Surname = "Surname20", Age = 20, Birthday = DateTime.Parse("01/01/2000"), Married = false });
            returnFirst.Add(new FirstModel { Name = "Name21", Surname = "Surname21", Age = 21, Birthday = DateTime.Parse("01/01/2001"), Married = false });
            returnFirst.Add(new FirstModel { Name = "Name22", Surname = "Surname22", Age = 22, Birthday = DateTime.Parse("01/01/2002"), Married = false });
            returnFirst.Add(new FirstModel { Name = "Name23", Surname = "Surname23", Age = 23, Birthday = DateTime.Parse("01/01/2003"), Married = false });
            returnFirst.Add(new FirstModel { Name = "Name24", Surname = "Surname24", Age = 24, Birthday = DateTime.Parse("01/01/2004"), Married = false });
            returnFirst.Add(new FirstModel { Name = "Name25", Surname = "Surname25", Age = 25, Birthday = DateTime.Parse("01/01/2005"), Married = false });
            returnFirst.Add(new FirstModel { Name = "Name26", Surname = "Surname26", Age = 26, Birthday = DateTime.Parse("01/01/2006"), Married = false });
            returnFirst.Add(new FirstModel { Name = "Name27", Surname = "Surname27", Age = 27, Birthday = DateTime.Parse("01/01/2007"), Married = false });
            returnFirst.Add(new FirstModel { Name = "Name28", Surname = "Surname28", Age = 28, Birthday = DateTime.Parse("01/01/2008"), Married = false });
            returnFirst.Add(new FirstModel { Name = "Name29", Surname = "Surname29", Age = 29, Birthday = DateTime.Parse("01/01/2009"), Married = false });
            returnFirst.Add(new FirstModel { Name = "Name30", Surname = "Surname30", Age = 30, Birthday = DateTime.Parse("01/01/2010"), Married = false });

            returnFirst.Add(new FirstModel { Name = "Name30", Surname = "Surname30", Age = 30, Birthday = DateTime.Parse("01/01/2010"), Married = true });
            returnFirst.Add(new FirstModel { Name = "Name31", Surname = "Surname31", Age = 31, Birthday = DateTime.Parse("01/01/2011"), Married = true });
            returnFirst.Add(new FirstModel { Name = "Name32", Surname = "Surname32", Age = 32, Birthday = DateTime.Parse("01/01/2012"), Married = true });
            returnFirst.Add(new FirstModel { Name = "Name33", Surname = "Surname33", Age = 33, Birthday = DateTime.Parse("01/01/2013"), Married = true });
            returnFirst.Add(new FirstModel { Name = "Name34", Surname = "Surname34", Age = 34, Birthday = DateTime.Parse("01/01/2014"), Married = true });
            returnFirst.Add(new FirstModel { Name = "Name35", Surname = "Surname35", Age = 35, Birthday = DateTime.Parse("01/01/2015"), Married = true });
            returnFirst.Add(new FirstModel { Name = "Name36", Surname = "Surname36", Age = 36, Birthday = DateTime.Parse("01/01/2016"), Married = true });
            returnFirst.Add(new FirstModel { Name = "Name37", Surname = "Surname37", Age = 37, Birthday = DateTime.Parse("01/01/2017"), Married = true });
            returnFirst.Add(new FirstModel { Name = "Name38", Surname = "Surname38", Age = 38, Birthday = DateTime.Parse("01/01/2018"), Married = true });
            returnFirst.Add(new FirstModel { Name = "Name39", Surname = "Surname39", Age = 39, Birthday = DateTime.Parse("01/01/2019"), Married = true });
            returnFirst.Add(new FirstModel { Name = "Name40", Surname = "Surname40", Age = 30, Birthday = DateTime.Parse("01/01/2020"), Married = true });

            return returnFirst;

        }              
    }

    
}