using Datalist.Web.Models;
using System;
using System.Linq;

namespace Datalist.Web.Context
{
    public static class PeopleRepository
    {
        private static IQueryable<Person> People { get; }

        static PeopleRepository()
        {
            People = new[]
            {
                new Person
                {
                    Id = 1,
                    Name = "Joe",
                    Surname = "Crosswave",

                    Birthday = new DateTime(1998, 09, 05),
                    IsWorking = false,
                    Income = null
                },
                new Person
                {
                    Id = 2,
                    Name = "Merry",
                    Surname = "Lisel",

                    Birthday = new DateTime(1982, 07, 25)
                },
                new Person
                {
                    Id = 3,
                    Name = "Henry",
                    Surname = "Crux",

                    Birthday = new DateTime(2009, 07, 14),
                    IsWorking = false,
                    Income = 0
                },
                new Person
                {
                    Id = 4,
                    Name = "Cody",
                    Surname = "Jurut",

                    Birthday = new DateTime(1930, 03, 03),
                    IsWorking = false,
                    Income = 0
                },
                new Person
                {
                    Id = 5,
                    Name = "Simon",
                    Surname = "Scranton",

                    Birthday = new DateTime(1982, 09, 22)
                },
                new Person
                {
                    Id = 6,
                    Name = "Leena",
                    Surname = "Laurent",

                    Birthday = new DateTime(1962, 07, 26),
                    IsWorking = false,
                    Income = 0
                },
                new Person
                {
                    Id = 7,
                    Name = "Ode",
                    Surname = "Cosmides",

                    Birthday = new DateTime(1974, 11, 02),
                    IsWorking = true,
                    Income = 1255
                },
                new Person
                {
                    Id = 8,
                    Name = "Diandra",
                    Surname = "Mizner",

                    Birthday = new DateTime(1954, 08, 15),
                    IsWorking = false,
                    Income = 0
                },
                new Person
                {
                    Id = 9,
                    Name = "Pete",
                    Surname = "Cassel",

                    Birthday = new DateTime(1977, 09, 10),
                    IsWorking = false,
                    Income = null
                },
                new Person
                {
                    Id = 10,
                    Name = "Nicky",
                    Surname = "Tremblay",

                    Birthday = new DateTime(1975, 01, 19),
                    IsWorking = true,
                    Income = 1120
                },
                new Person
                {
                    Id = 11,
                    Name = "Lisa",
                    Surname = "Tremblay",

                    Birthday = new DateTime(1992, 04, 29),
                    IsWorking = true,
                    Income = 700
                },
                new Person
                {
                    Id = 12,
                    Name = "Claybourne",
                    Surname = "Siekkinen",

                    Birthday = new DateTime(1986, 01, 03),
                    IsWorking = true,
                    Income = 670
                },
                new Person
                {
                    Id = 13,
                    Name = "Karoline",
                    Surname = "Salisbury",

                    Birthday = new DateTime(1937, 06, 21),
                    IsWorking = true,
                    Income = 660
                },
                new Person
                {
                    Id = 14,
                    Name = "Lanny",
                    Surname = "Tempesta",

                    Birthday = new DateTime(2011, 02, 18),
                    IsWorking = false,
                    Income = 0
                },
                new Person
                {
                    Id = 15,
                    Name = "Brandice",
                    Surname = "Strassoldo",

                    Birthday = new DateTime(2002, 02, 25),
                    IsWorking = true,
                    Income = 100
                },
                new Person
                {
                    Id = 16,
                    Name = "Gannie",
                    Surname = "Diaconis",

                    Birthday = new DateTime(1960, 06, 06),
                    IsWorking = false,
                    Income = 0
                },
                new Person
                {
                    Id = 17,
                    Name = "Ferdinanda",
                    Surname = "Mcfeeley",

                    Birthday = new DateTime(1945, 07, 09)
                },
                new Person
                {
                    Id = 18,
                    Name = "Jamey",
                    Surname = "Temple",

                    Birthday = new DateTime(1991, 07, 26)
                },
                new Person
                {
                    Id = 19,
                    Name = "Anette",
                    Surname = "Daiute",

                    Birthday = new DateTime(2002, 06, 03),
                    IsWorking = false,
                    Income = 0
                },
                new Person
                {
                    Id = 20,
                    Name = "Vick",
                    Surname = "Read",

                    Birthday = new DateTime(1987, 01, 25),
                    IsWorking = true,
                    Income = 800
                },
                new Person
                {
                    Id = 21,
                    Name = "Tatiania",
                    Surname = "Conroy",

                    Birthday = new DateTime(1974, 05, 17),
                    IsWorking = true,
                    Income = 880
                },
                new Person
                {
                    Id = 22,
                    Name = "Arman",
                    Surname = "Kotelchuk",

                    Birthday = new DateTime(2002, 03, 26),
                    IsWorking = true,
                    Income = 150
                },
                new Person
                {
                    Id = 23,
                    Name = "Berni",
                    Surname = "Delger",

                    Birthday = new DateTime(1937, 11, 23),
                    IsWorking = true,
                    Income = 460
                },
                new Person
                {
                    Id = 24,
                    Name = "Hammad",
                    Surname = "Jay",

                    Birthday = new DateTime(1987, 04, 02),
                    IsWorking = true,
                    Income = 555
                },
                new Person
                {
                    Id = 25,
                    Name = "Gwenora",
                    Surname = "Boym",

                    Birthday = new DateTime(1944, 07, 23)
                },
                new Person
                {
                    Id = 26,
                    Name = "Ichabod",
                    Surname = "Parmeggiani",

                    Birthday = new DateTime(1968, 05, 15),
                    IsWorking = true,
                    Income = 990
                },
                new Person
                {
                    Id = 27,
                    Name = "Layne",
                    Surname = "Burnham",

                    Birthday = new DateTime(1987, 03, 09),
                    IsWorking = true,
                    Income = 750
                },
                new Person
                {
                    Id = 28,
                    Name = "Hervey",
                    Surname = "Mckeown",

                    Birthday = new DateTime(1941, 04, 13),
                    IsWorking = false,
                    Income = 0
                },
                new Person
                {
                    Id = 29,
                    Name = "Carita",
                    Surname = "Sumner",

                    Birthday = new DateTime(1956, 01, 21),
                    IsWorking = false,
                    Income = 0
                },
                new Person
                {
                    Id = 30,
                    Name = "Victoir",
                    Surname = "Shelton",

                    Birthday = new DateTime(1940, 09, 16),
                    IsWorking = true,
                    Income = 880
                },
                new Person
                {
                    Id = 31,
                    Name = "Peg",
                    Surname = "Chao",

                    Birthday = new DateTime(1930, 05, 14),
                    IsWorking = true,
                    Income = 990
                },
                new Person
                {
                    Id = 32,
                    Name = "Tabbie",
                    Surname = "Parisi",

                    Birthday = new DateTime(2003, 05, 22),
                    IsWorking = true,
                    Income = 100
                },
                new Person
                {
                    Id = 33,
                    Name = "Arlina",
                    Surname = "Brechbuhl",

                    Birthday = new DateTime(1935, 01, 27),
                    IsWorking = true,
                    Income = 2250
                },
                new Person
                {
                    Id = 34,
                    Name = "Sutton",
                    Surname = "Milner",

                    Birthday = new DateTime(2010, 03, 16),
                    IsWorking = false,
                    Income = 0
                },
                new Person
                {
                    Id = 35,
                    Name = "Ashlan",
                    Surname = "Smoljanovic",

                    Birthday = new DateTime(1967, 06, 10),
                    IsWorking = true,
                    Income = 900
                },
                new Person
                {
                    Id = 36,
                    Name = "Garrick",
                    Surname = "Carini",

                    Birthday = new DateTime(1952, 06, 02),
                    IsWorking = false,
                    Income = 0
                },
                new Person
                {
                    Id = 37,
                    Name = "Nerita",
                    Surname = "Lancaster",

                    Birthday = new DateTime(1937, 05, 03),
                    IsWorking = false,
                    Income = 0
                },
                new Person
                {
                    Id = 38,
                    Name = "Sergeant",
                    Surname = "Strassoldo",

                    Birthday = new DateTime(1979, 11, 26)
                },
                new Person
                {
                    Id = 39,
                    Name = "Elly",
                    Surname = "Arvoy",

                    Birthday = new DateTime(1978, 05, 02),
                    IsWorking = true,
                    Income = 830
                },
                new Person
                {
                    Id = 40,
                    Name = "Elisha",
                    Surname = "Daiute",

                    Birthday = new DateTime(1941, 06, 08),
                    IsWorking = true,
                    Income = 470
                },
                new Person
                {
                    Id = 41,
                    Name = "Ilise",
                    Surname = "Schervish",

                    Birthday = new DateTime(1945, 11, 05),
                    IsWorking = true,
                    Income = 680
                },
                new Person
                {
                    Id = 42,
                    Name = "Page",
                    Surname = "Delger",

                    Birthday = new DateTime(1950, 08, 26),
                    IsWorking = false,
                    Income = 0
                },
                new Person
                {
                    Id = 43,
                    Name = "Annis",
                    Surname = "Vichniac",

                    Birthday = new DateTime(1978, 04, 04),
                    IsWorking = true,
                    Income = 540
                },
                new Person
                {
                    Id = 44,
                    Name = "Ambrosi",
                    Surname = "Burnham",

                    Birthday = new DateTime(1934, 05, 13),
                    IsWorking = false,
                    Income = 0
                },
                new Person
                {
                    Id = 45,
                    Name = "Robinett",
                    Surname = "Salsberg",

                    Birthday = new DateTime(1980, 09, 27),
                    IsWorking = true,
                    Income = 780
                },
                new Person
                {
                    Id = 46,
                    Name = "Lionello",
                    Surname = "Chao",

                    Birthday = new DateTime(1995, 08, 25),
                    IsWorking = false,
                    Income = 0
                },
                new Person
                {
                    Id = 47,
                    Name = "Helge",
                    Surname = "Worton",

                    Birthday = new DateTime(1938, 01, 14),
                    IsWorking = false,
                    Income = 0
                },
                new Person
                {
                    Id = 48,
                    Name = "Rollins",
                    Surname = "Smoljanovic",

                    Birthday = new DateTime(1986, 01, 12),
                    IsWorking = true,
                    Income = 1050
                },
                new Person
                {
                    Id = 49,
                    Name = "Kitti",
                    Surname = "Chiaramonte",

                    Birthday = new DateTime(1968, 05, 13),
                    IsWorking = true,
                    Income = 500
                },
                new Person
                {
                    Id = 50,
                    Name = "Gale",
                    Surname = "Arvoy",

                    Birthday = new DateTime(1981, 04, 02),
                    IsWorking = true,
                    Income = 540
                },
                new Person
                {
                    Id = 51,
                    Name = "Guinevere",
                    Surname = "Froot",

                    Birthday = new DateTime(1990, 11, 14),
                    IsWorking = true,
                    Income = 1100
                },
                new Person
                {
                    Id = 52,
                    Name = "Harman",
                    Surname = "Vichniac",

                    Birthday = new DateTime(1985, 09, 09),
                    IsWorking = false,
                    Income = 0
                }
            }.AsQueryable();
        }

        public static IQueryable<Person> Get()
        {
            return People;
        }
    }
}
