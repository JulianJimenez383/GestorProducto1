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
    public class ProveedorsController : Controller
    {
        private InventarioDesarrolloWebEntities db = new InventarioDesarrolloWebEntities();
        GestorRepository<Proveedor> data = new GestorRepository<Proveedor>();
        GestorRepository<GuardarM> da = new GestorRepository<GuardarM>();


        // GET: Proveedors
        public async Task<ActionResult> Index()
        {
            var usuario = Session["usuario"] as Usuario;
            var proveedor = await db.Proveedor
               .Include(b => b.Usuario)
               .ToListAsync();
            return View(proveedor.ToList());
        }

        // GET: Proveedors/Details/5
        public async Task <ActionResult> Details(string id)
        {
            var usuario = Session["usuario"] as Usuario;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proveedor proveedor = await data.GetById(id);
            if (proveedor == null)
            {
                return HttpNotFound();
            }
            return View(proveedor);
        }

        // GET: Proveedors/Create
        public ActionResult Create()
        {
            var usuario = Session["usuario"] as Usuario;
            ViewBag.IdUsuario = new SelectList(db.Usuario, "IdUsuario", "IdUsuario");
            return View();
        }

        // POST: Proveedors/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IdProveedor,NombreProveedor,DireccionProveedor,TelefonoProveedor,CorreoProveedor,IdUsuario")] Proveedor proveedor)
        {
            var usuario = Session["usuario"] as Usuario;
            if (ModelState.IsValid)
            {
                await data.Create(proveedor);
                db.SaveChanges();

                var mov = new GuardarM
                {

                    FechaModificacion = DateTime.Now.ToString("dd / MM / yyyy hh: mm:ss tt"),
                    IdUsuario = usuario.IdUsuario,
                    NombreUsuario = usuario.NombreUsuario,
                    IdProducto = proveedor.IdProveedor,
                    NombreProducto = proveedor.NombreProveedor,
                    AccionModificacion = "CREACION"
                };
                await da.CreateGuardarM(mov);
                return RedirectToAction("Index");
            }

            ViewBag.IdUsuario = new SelectList(db.Usuario, "IdUsuario", "Password", proveedor.IdUsuario);
            return View(proveedor);
        }

        // GET: Proveedors/Edit/5
        public async Task <ActionResult> Edit(string id)
        {
            var usuario = Session["usuario"] as Usuario;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proveedor proveedor = await data.GetById(id);
            if (proveedor == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdUsuario = new SelectList(db.Usuario, "IdUsuario", "Password", proveedor.IdUsuario);
            return View(proveedor);
        }

        // POST: Proveedors/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <ActionResult> Edit([Bind(Include = "IdProveedor,NombreProveedor,DireccionProveedor,TelefonoProveedor,CorreoProveedor,IdUsuario")] Proveedor proveedor)
        {
            var usuario = Session["usuario"] as Usuario;
            if (ModelState.IsValid)
            {
                await data.Update(proveedor); 
                db.SaveChanges();

                var mov = new GuardarM
                {

                    FechaModificacion = DateTime.Now.ToString("dd / MM / yyyy hh: mm:ss tt"),
                    IdUsuario = usuario.IdUsuario,
                    NombreUsuario = usuario.NombreUsuario,
                    IdProducto = proveedor.IdProveedor,
                    NombreProducto = proveedor.NombreProveedor,
                    AccionModificacion = "EDICION"
                };
                await da.CreateGuardarM(mov);


                return RedirectToAction("Index");
            }
            ViewBag.IdUsuario = new SelectList(db.Usuario, "IdUsuario", "Password", proveedor.IdUsuario);
            return View(proveedor);
        }

        // GET: Proveedors/Delete/5
        public async Task <ActionResult> Delete(string id)
        {
            var usuario = Session["usuario"] as Usuario;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Proveedor proveedor = await data.GetById(id);
            if (proveedor == null)
            {
                return HttpNotFound();
            }
            return View(proveedor);
        }

        // POST: Proveedors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task <ActionResult> DeleteConfirmed(string id)
        {
            var usuario = Session["usuario"] as Usuario;
            Proveedor proveedor = await data.GetById(id);
            await data.Delete(proveedor);
            db.SaveChanges();

            var mov = new GuardarM
            {

                FechaModificacion = DateTime.Now.ToString("dd / MM / yyyy hh: mm:ss tt"),
                IdUsuario = usuario.IdUsuario,
                NombreUsuario = usuario.NombreUsuario,
                IdProducto = proveedor.IdProveedor,
                NombreProducto = proveedor.NombreProveedor,
                AccionModificacion = "ELIMINACION"
            };
            await da.CreateGuardarM(mov);

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
