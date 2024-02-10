using MVC5Test.Components.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace MVC5Test.Objects
{
    public class ProfileDeleteView : BaseView
    {
        [Required]
        [NotTrimmed]
        [StringLength(32)]
        public String Password { get; set; }
    }
}
