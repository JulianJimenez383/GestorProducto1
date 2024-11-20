using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestorProducto1.Controllers
{
    public class RecuperarContraseñaController : Controller
    {
        // GET: RecuperarContraseña
        public ActionResult Index()
        {
            return View();
        }

        // GET: RecuperarContraseña/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RecuperarContraseña/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RecuperarContraseña/Create
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

        // GET: RecuperarContraseña/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RecuperarContraseña/Edit/5
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

        // GET: RecuperarContraseña/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RecuperarContraseña/Delete/5
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
