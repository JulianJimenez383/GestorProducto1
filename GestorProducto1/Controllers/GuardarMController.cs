using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GestorProductoBack.Model;

namespace GestorProducto1.Controllers
{
    public class GuardarMController : Controller
    {
        private InventarioDesarrolloWebEntities db = new InventarioDesarrolloWebEntities();

        // GET: GuardarM
        public ActionResult Index()
        {
            var usuario = Session["usuario"] as Usuario;
            ViewBag.Nombre = usuario.NombreUsuario;
            var guardarM = db.GuardarM.Include(g => g.Usuario);
            return View(guardarM.ToList());
        }

        // GET: GuardarM/Details/5
        public ActionResult Details(int? id)
        {
            var usuario = Session["usuario"] as Usuario;
            ViewBag.Nombre = usuario.NombreUsuario;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GuardarM guardarM = db.GuardarM.Find(id);
            if (guardarM == null)
            {
                return HttpNotFound();
            }
            return View(guardarM);
        }

        // GET: GuardarM/Create
        public ActionResult Create()
        {
            ViewBag.IdUsuario = new SelectList(db.Usuario, "IdUsuario", "Password");
            return View();
        }

        // POST: GuardarM/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdModificar,FechaModificacion,IdUsuario,NombreUsuario,IdProducto,NombreProducto,AccionModificacion")] GuardarM guardarM)
        {
            if (ModelState.IsValid)
            {
                db.GuardarM.Add(guardarM);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdUsuario = new SelectList(db.Usuario, "IdUsuario", "Password", guardarM.IdUsuario);
            return View(guardarM);
        }

        // GET: GuardarM/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GuardarM guardarM = db.GuardarM.Find(id);
            if (guardarM == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdUsuario = new SelectList(db.Usuario, "IdUsuario", "Password", guardarM.IdUsuario);
            return View(guardarM);
        }

        // POST: GuardarM/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdModificar,FechaModificacion,IdUsuario,NombreUsuario,IdProducto,NombreProducto,AccionModificacion")] GuardarM guardarM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(guardarM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdUsuario = new SelectList(db.Usuario, "IdUsuario", "Password", guardarM.IdUsuario);
            return View(guardarM);
        }

        // GET: GuardarM/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GuardarM guardarM = db.GuardarM.Find(id);
            if (guardarM == null)
            {
                return HttpNotFound();
            }
            return View(guardarM);
        }

        // POST: GuardarM/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GuardarM guardarM = db.GuardarM.Find(id);
            db.GuardarM.Remove(guardarM);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
