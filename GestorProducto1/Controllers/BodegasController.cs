using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using GestorProductoBack.Model;
using GestorProductoBack.Repository;

namespace GestorProducto1.Controllers
{
    public class BodegasController : Controller
    {
        private InventarioDesarrolloWebEntities db = new InventarioDesarrolloWebEntities();
        GestorRepository<Bodega> data = new GestorRepository<Bodega>();

        // GET: Bodegas
        public async Task <ActionResult> Index()
        {
            //var temoral
            var usuario = Session["usuario"] as Usuario;
            ViewBag.Nombre = usuario.NombreUsuario;

            var bodega = await db.Bodega
                .Include(b => b.Usuario)
                .ToListAsync();
                return View(bodega.ToList());
        }

        // GET: Bodegas/Details/5
        public async Task <ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bodega bodega = await data.GetById(id);
            if (bodega == null)
            {
                return HttpNotFound();
            }
            return View(bodega);
        }

        // GET: Bodegas/Create
        public async Task<ActionResult> Create()
        {
            var bodegas = await db.Bodega.ToListAsync();
            var usuarios = await db.Usuario.ToListAsync();

            ViewBag.IdUsuario = new SelectList(usuarios, "IdUsuario", "IdUsuario"); // Cambia "Password" por un campo más apropiado.

            return View();
        }

        // POST: Bodegas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <ActionResult> Create([Bind(Include = "IdBodega,NombreBodega,PaisBodega,DepartamentoBodega,CiudadBodega,IdUsuario")] Bodega bodega)
        {
            if (ModelState.IsValid)
            {
                await data.Create(bodega);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdUsuario = new SelectList(db.Usuario, "IdUsuario", "Password", bodega.IdUsuario);
            return View(bodega);
        }

        // GET: Bodegas/Edit/5
        public async Task <ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bodega bodega = await data.GetById(id);
            if (bodega == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdUsuario = new SelectList(db.Usuario, "IdUsuario", "IdUsuario", bodega.IdUsuario);
            return View(bodega);
        }

        // POST: Bodegas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <ActionResult> Edit([Bind(Include = "IdBodega,NombreBodega,PaisBodega,DepartamentoBodega,CiudadBodega,IdUsuario")] Bodega bodega)
        {
            if (ModelState.IsValid)
            {
                await data.Update(bodega);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdUsuario = new SelectList(db.Usuario, "IdUsuario", "Password", bodega.IdUsuario);
            return View(bodega);
        }

        // GET: Bodegas/Delete/5
        public async Task <ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bodega bodega = await data.GetById(id);
            if (bodega == null)
            {
                return HttpNotFound();
            }
            return View(bodega);
        }

        // POST: Bodegas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task  <ActionResult> DeleteConfirmed(string id)
        {
            Bodega bodega = await data.GetById(id);
            await data.Delete(bodega);
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
