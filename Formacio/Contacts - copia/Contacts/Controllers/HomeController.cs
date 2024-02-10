using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Contacts.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Test table with comments related";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Its me";

            return View();
        }
    }
}