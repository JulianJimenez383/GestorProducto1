using GestorProductoBack.Model;
using GestorProductoBack.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;

namespace GestorProducto1.Controllers
{
    public class RecuperarContraseñaController : Controller
    {
        private InventarioDesarrolloWebEntities db = new InventarioDesarrolloWebEntities();
        GestorRepository<Usuario> data = new GestorRepository<Usuario>();
        // GET: RecuperarContraseña
        public ActionResult Index(string cedula, string contra, string contra1)
        {
            if (string.IsNullOrEmpty(cedula) || string.IsNullOrEmpty(contra) || string.IsNullOrEmpty(contra1))
            {
                ViewBag.Error = "Todos loc campos son obligatorios";
                return View();
            }

            // Buscar al usuario en la base de datos
            var u = db.Usuario.FirstOrDefault(x => x.IdUsuario == cedula);
            var c = db.Usuario.FirstOrDefault(x => x.Password == contra);

            if (u != null)
            {
                if (u.IdUsuario == cedula )
                {
                    
                    if(contra == contra1)
                    {
                        u.Password = contra1;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    return RedirectToAction("Index", "Login");
                }
            }

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
