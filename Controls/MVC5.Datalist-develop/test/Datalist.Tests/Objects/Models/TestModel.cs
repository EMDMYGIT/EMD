﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Datalist.Tests.Objects
{
    public class TestModel
    {
        [DatalistColumn(-3, Hidden = true)]
        public String Id { get; set; }

        [DatalistColumn(8)]
        [Display(Name = "Count's value", ShortName = "Value")]
        public Int32 Count { get; set; }

        [DatalistColumn]
        public String Value { get; set; }

        public String ParentId { get; set; }

        [Display(Name = "Date")]
        [DatalistColumn(3, Format = "{0:d}")]
        public DateTime Date { get; set; }

        public String[] Values { get; set; }
    }
}
