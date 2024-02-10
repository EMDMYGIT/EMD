using System;
using System.Collections.Generic;
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
        #endregion
        // GET: TB_Contact
        public ActionResult Index(string sortOrder)
        {
            ViewBag.NameSortParm = sortOrder== "Name" ? "Name desc" : "Name";
            listContact = dbUtils.RetrieveContactData(0,sortOrder);
            return View("Index", listContact);
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
                return RedirectToAction("Index");
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
                return RedirectToAction("Index");
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
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
