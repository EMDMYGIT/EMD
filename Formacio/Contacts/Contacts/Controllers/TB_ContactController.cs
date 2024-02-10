using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Contacts.Controllers
{
    public class TB_ContactController : Controller
    {
        #region Declaration
        DBClases.clsDtaUtils dbUtils = new DBClases.clsDtaUtils();
        List<Contacts.Models.TB_Contact> listContact = new List<Models.TB_Contact>();
        List<Contacts.Models.TB_Comments> listComments = new List<Models.TB_Comments>();
        Models.ErrorModels sendError = new Models.ErrorModels();
        #endregion
        // GET: TB_Contact
        public ActionResult Index(string sortOrder)
        {
            if (1 == 2)
            {
#pragma warning disable
                ViewBag.NameSortParm = sortOrder == "Name" ? "Name desc" : "Name";
                listContact = dbUtils.RetrieveContactData(0, sortOrder);
                return View("Index", listContact);
            }
            else { 
                sendError.TextError = "This screen is discontinued";
                sendError.returnContrtoler = "Home";
                sendError.returnAction = "Index";
                TempData["sendError"] = sendError;
                return RedirectToAction("ViewError", "SendError");
            }
        }
        public ActionResult IndexGrid(string sortOrder)
        {
            ViewBag.NameSortParm = sortOrder == "Name" ? "Name desc" : "Name";
            listContact = dbUtils.RetrieveContactData(0, sortOrder);
            return View("IndexGrid", listContact);
        }

        // GET: TB_Contact related data
        public ActionResult Select()
        {
            listContact = dbUtils.RetrieveContactDataRelated(" and City='");
            return View("Index", listContact);
        }

        // GET: TB_Contact/Details/5
        public ActionResult Details(int id)
        {
            listContact = dbUtils.RetrieveContactData(id,"");
            return View("Details",listContact[0]);
        }

        // GET: TB_Contact/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TB_Contact/Create
        [HttpPost]
        public ActionResult Create(Contacts.Models.TB_Contact insItem)//FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                dbUtils.InsertContactsData(insItem);
                return RedirectToAction("IndexGrid");
            }
            catch
            {
                return View();
            }
        }

        // GET: TB_Contact/Edit/5
        public ActionResult Edit(int id)
        {
            listContact = dbUtils.RetrieveContactData(id,"");
            return View("Edit",listContact[0]);
        }

        // POST: TB_Contact/Edit/5
        [HttpPost]
        public ActionResult Edit(int id,Contacts.Models.TB_Contact editItem )//FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                dbUtils.UpdateContactsData(editItem);
                return RedirectToAction("IndexGrid");
            }
            catch
            {
                return View();
            }
        }

        // GET: TB_Contact/Delete/5
        public ActionResult Delete(int id)
        {
            listContact = dbUtils.RetrieveContactData(id,"");
            return View("Delete",listContact[0]);
        }

        // POST: TB_Contact/Delete/5
        [HttpPost]
        public ActionResult Delete(int id,Contacts.Models.TB_Contact delItem)// FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                
                dbUtils.DeleteContacData(id);
                return RedirectToAction("IndexGrid");
            }
            catch
            {
                return View();
            }
        }
        //Filters
        [HttpPost]
        public JsonResult GetCities()
        {
            var allItems = dbUtils.RetrieveContactCitiesGrid("").Select(c => c.CityName);
            return Json(new { Items = allItems });
        }
        public ActionResult Comments()
        {
            if (ViewBag.SelectedRowID != null)
            {
                listComments = dbUtils.RetrieveContactComment(ViewBag.SelectedRowID);

                return PartialView("Comments", listComments[0]);
            }
            else { return View("IndexGrid"); }
        }
        [HttpPost]
        public JsonResult GetContact(int id)
        {

            //listComments = dbUtils.RetrieveContactComment(id);
            Models.TB_Comments modComments = dbUtils.RetrieveContactComment(id);
            
            if (listComments == null)
                return Json(new { Status = 0, Message = "Not found" });

            return Json(new { Status = 1, Message = "Ok", Content = RenderPartialViewToString("Comments", modComments) });
        }
        protected string RenderPartialViewToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.RouteData.GetRequiredString("action");

            ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }
    }
   
}
