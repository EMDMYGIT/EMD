using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Contacts.Views
{
    public class SendErrorController : Controller
    {
        // GET: Error
        public ActionResult ViewError()
        {
            var pError = TempData["sendError"] as Models.ErrorModels; 
            return View("ShowError",pError);
        }
    }
}