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
    public class GuardarTController : Controller
    {
        private InventarioDesarrolloWebEntities db = new InventarioDesarrolloWebEntities();

        // GET: GuardarT
        public ActionResult Index()
        {
            var usuario = Session["usuario"] as Usuario;
            ViewBag.Nombre = usuario.NombreUsuario;
            var guardarT = db.GuardarT.Include(g => g.Producto).Include(g => g.Usuario);
            return View(guardarT.ToList());
        }

        // GET: GuardarT/Details/5
        public ActionResult Details(string id)
        {
            var usuario = Session["usuario"] as Usuario;
            ViewBag.Nombre = usuario.NombreUsuario;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GuardarT guardarT = db.GuardarT.Find(id);
            if (guardarT == null)
            {
                return HttpNotFound();
            }
            return View(guardarT);
        }

        // GET: GuardarT/Create
        public ActionResult Create()
        {
            var usuario = Session["usuario"] as Usuario;
            ViewBag.Nombre = usuario.NombreUsuario;
            ViewBag.IdProducto = new SelectList(db.Producto, "IdProducto", "IdBodega");
            ViewBag.IdUsuario = new SelectList(db.Usuario, "IdUsuario", "Password");
            return View();
        }

        // POST: GuardarT/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdTransaccion,IdProducto,CantidadProducto,FechachaTransaccion,IdUsuario,NombreUsuario,ApellidoUsuario,IdBodegaOrigen,IdBodegaDestino")] GuardarT guardarT)
        {
            var usuario = Session["usuario"] as Usuario;
            ViewBag.Nombre = usuario.NombreUsuario;
            if (ModelState.IsValid)
            {
                db.GuardarT.Add(guardarT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdProducto = new SelectList(db.Producto, "IdProducto", "IdBodega", guardarT.IdProducto);
            ViewBag.IdUsuario = new SelectList(db.Usuario, "IdUsuario", "Password", guardarT.IdUsuario);
            return View(guardarT);
        }

        // GET: GuardarT/Edit/5
        public ActionResult Edit(string id)
        {
            var usuario = Session["usuario"] as Usuario;
            ViewBag.Nombre = usuario.NombreUsuario;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GuardarT guardarT = db.GuardarT.Find(id);
            if (guardarT == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdProducto = new SelectList(db.Producto, "IdProducto", "IdBodega", guardarT.IdProducto);
            ViewBag.IdUsuario = new SelectList(db.Usuario, "IdUsuario", "Password", guardarT.IdUsuario);
            return View(guardarT);
        }

        // POST: GuardarT/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdTransaccion,IdProducto,CantidadProducto,FechachaTransaccion,IdUsuario,NombreUsuario,ApellidoUsuario,IdBodegaOrigen,IdBodegaDestino")] GuardarT guardarT)
        {
            var usuario = Session["usuario"] as Usuario;
            ViewBag.Nombre = usuario.NombreUsuario;
            if (ModelState.IsValid)
            {
                db.Entry(guardarT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdProducto = new SelectList(db.Producto, "IdProducto", "IdBodega", guardarT.IdProducto);
            ViewBag.IdUsuario = new SelectList(db.Usuario, "IdUsuario", "Password", guardarT.IdUsuario);
            return View(guardarT);
        }

        // GET: GuardarT/Delete/5
        public ActionResult Delete(string id)
        {
            var usuario = Session["usuario"] as Usuario;
            ViewBag.Nombre = usuario.NombreUsuario;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GuardarT guardarT = db.GuardarT.Find(id);
            if (guardarT == null)
            {
                return HttpNotFound();
            }
            return View(guardarT);
        }

        // POST: GuardarT/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var usuario = Session["usuario"] as Usuario;
            ViewBag.Nombre = usuario.NombreUsuario;
            GuardarT guardarT = db.GuardarT.Find(id);
            db.GuardarT.Remove(guardarT);
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
