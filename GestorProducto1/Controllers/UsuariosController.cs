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
    public class UsuariosController : Controller
    {
        private InventarioDesarrolloWebEntities db = new InventarioDesarrolloWebEntities();
        GestorRepository<Usuario> data = new GestorRepository<Usuario>();
        GestorRepository<GuardarM> da = new GestorRepository<GuardarM>();

        // GET: ver Usuarios
        public async Task<ActionResult> Index()
        {
            //var temoral

            var usu = Session["usuario"] as Usuario;
            ViewBag.Nombre = usu.NombreUsuario;

            var usuarios = await db.Usuario.ToListAsync();
            return View(db.Usuario.ToList());
        }


        // GET: Usuarios/Details/5
        public async Task<ActionResult> Details(string id)
        {
            var usu = Session["usuario"] as Usuario;
            ViewBag.Nombre = usu.NombreUsuario;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = await data.GetById(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IdUsuario,Password,NombreUsuario,ApellidoUsuario,CorreoUsuario,FechaNaciUsuario,CargoUsuario")] Usuario usuario)
        {
            var usu = Session["usuario"] as Usuario;
            ViewBag.Nombre = usu.NombreUsuario;
            if (ModelState.IsValid)
            {
                await data.Create(usuario);

                var mov = new GuardarM
                {

                    FechaModificacion = DateTime.Now.ToString("dd / MM / yyyy hh: mm:ss tt"),
                    IdUsuario = usu.IdUsuario,
                    NombreUsuario = usu.NombreUsuario,
                    IdProducto = usuario.IdUsuario,
                    NombreProducto = usuario.NombreUsuario,
                    AccionModificacion = "CREACION USUARIO"
                };
                await da.CreateGuardarM(mov);
                return RedirectToAction("Index");
            }

            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            var usu = Session["usuario"] as Usuario;
            ViewBag.Nombre = usu.NombreUsuario;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = await data.GetById(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IdUsuario,Password,NombreUsuario,ApellidoUsuario,CorreoUsuario,FechaNaciUsuario,CargoUsuario")] Usuario usuario)
        {
            var usu = Session["usuario"] as Usuario;
            ViewBag.Nombre = usu.NombreUsuario;
            if (ModelState.IsValid)
            {
                await data.Update(usuario);

                var mov = new GuardarM
                {

                    FechaModificacion = DateTime.Now.ToString("dd / MM / yyyy hh: mm:ss tt"),
                    IdUsuario = usu.IdUsuario,
                    NombreUsuario = usu.NombreUsuario,
                    IdProducto = usuario.IdUsuario,
                    NombreProducto = usuario.NombreUsuario,
                    AccionModificacion = "EDICION USUARIO"
                };
                await da.CreateGuardarM(mov);


                return RedirectToAction("Index");
            }
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            var usu = Session["usuario"] as Usuario;
            ViewBag.Nombre = usu.NombreUsuario;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = await data.GetById(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            var usu = Session["usuario"] as Usuario;
            ViewBag.Nombre = usu.NombreUsuario;
            Usuario usuario = await data.GetById(id);
            await data.Delete(usuario);

            var mov = new GuardarM
            {

                FechaModificacion = DateTime.Now.ToString("dd / MM / yyyy hh: mm:ss tt"),
                IdUsuario = usu.IdUsuario,
                NombreUsuario = usu.NombreUsuario,
                IdProducto = usuario.IdUsuario,
                NombreProducto = usuario.NombreUsuario,
                AccionModificacion = "ELIMINACION USUARIO"
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




