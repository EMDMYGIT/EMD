using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace TestMVC1.Controllers
{
    public class Test1Controller : Controller
    {
#region Declarations
        DBClases.clsDtaUtils dbUtils = new DBClases.clsDtaUtils();
        List<TestMVC1.Models.Test1Model> dataTest1 = new List<Models.Test1Model>();
#endregion
        // GET: Test1
        // Carga lista de test1 (Grid)
        public ActionResult Index()
        {
            dataTest1=dbUtils.RetrieveTest1Data(0);
            return View("Index",dataTest1);
        }

        // GET: Test1/Details/5
        // Consulta de Test1 (muestra los datos de un resgitro
        public ActionResult Details(int id)
        {
            dataTest1 = dbUtils.RetrieveTest1Data(id);
            return View("Details", dataTest1[0]);
        }

        // GET: Test1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Test1/Create
        [HttpPost]
        public ActionResult Create(Models.Test1Model dataTest1 ) // (FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                dbUtils.InsertTest1Data(dataTest1);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Test1/Edit/5
        // Carga de datos en detalle
        public ActionResult Edit(int id)
        {
            dataTest1 = dbUtils.RetrieveTest1Data(id);
            return View("Edit",dataTest1[0]);
        }

        // POST: Test1/Edit/5
        // Grabar los datos desde detalle
        [HttpPost]
        public ActionResult Edit(int id , Models.Test1Model dataTest1)//(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                dbUtils.UpdateTest1Data(dataTest1);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Test1/Delete/5
        public ActionResult Delete(int id)
        {
            dataTest1 = dbUtils.RetrieveTest1Data(id);
            return View("Delete",dataTest1[0]);
        }

        // POST: Test1/Delete/5
        [HttpPost]
        public ActionResult Delete(int id,Models.Test1Model dataTest1)//(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                dbUtils.DeleteTest1Data(dataTest1);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
