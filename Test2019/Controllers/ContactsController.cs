﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Test2019.Controllers
{
    public class ContactsController : Controller
    {
        #region Declaration
        DBUtils.clsUtils dbUtils = new DBUtils.clsUtils();
        List<Test2019.Models.TB_Contact> listContact = new List<Models.TB_Contact>();
        #endregion
        // GET: Contacts
        public ActionResult Index()
        {
                string sortOrder = "";
                ViewBag.NameSortParm = sortOrder == "Name" ? "Name desc" : "Name";
                listContact = dbUtils.RetrieveContactData(0, sortOrder);
                return View("Index", listContact);
        }
         
        

        // GET: Contacts/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Contacts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Contacts/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Contacts/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Contacts/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Contacts/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Contacts/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
