using MVC5Test.Objects;
using System;
using System.ComponentModel.DataAnnotations;

namespace MVC5Test.Tests.Objects
{
    public class TestModel : BaseModel
    {
        [StringLength(128)]
        public String Title { get; set; }
    }
}
