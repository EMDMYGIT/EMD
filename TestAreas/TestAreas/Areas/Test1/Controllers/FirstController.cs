using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestAreas.Areas.Test1.Controllers
{
    public class FirstController : Controller
    {
        public Models.DataLoad getData = new Models.DataLoad();
        public List<Models.FirstModel> griddata = new List<Models.FirstModel>();
        #region NormalGrid
        public ActionResult IndexGrid()
        {
            griddata = getData.Persons();
            return View(griddata);
        }

        // GET: Test1/First/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Test1/First/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Test1/First/Create
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

        // GET: Test1/First/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Test1/First/Edit/5
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

        // GET: Test1/First/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Test1/First/Delete/5
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
        #endregion
        #region Devexpress
        // GET: Test1/First
        public ActionResult Index()
        {
            //This view renders grid partial view.
            return View("Index");
        }

        // DEVEXPRESS Wizard
        [ValidateInput(false)]
        public ActionResult GridViewPartial()
        {
            griddata = getData.Persons();
            return PartialView("_GridViewPartial", griddata);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] TestAreas.Areas.Test1.Models.FirstModel item)
        {
            var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] TestAreas.Areas.Test1.Models.FirstModel item)
        {
            var model = new object[0];
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to update the item in your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialDelete(System.String Name)
        {
            var model = new object[0];
            if (Name != null)
            {
                try
                {
                    // Insert here a code to delete the item from your model
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_GridViewPartial", model);
        }
        // Devexpress wizard.
#endregion
    }
}
