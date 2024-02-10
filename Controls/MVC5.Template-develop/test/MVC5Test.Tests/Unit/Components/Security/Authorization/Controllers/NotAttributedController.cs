﻿using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;

namespace MVC5Test.Tests.Unit.Components.Security
{
    [ExcludeFromCodeCoverage]
    public class NotAttributedController : Controller
    {
        [HttpGet]
        public ViewResult Action()
        {
            return null;
        }
    }
}
