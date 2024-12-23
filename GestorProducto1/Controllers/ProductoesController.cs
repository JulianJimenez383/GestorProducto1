﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using GestorProductoBack.Model;
using GestorProductoBack.Repository;


namespace GestorProducto1.Controllers
{
    public class ProductoesController : Controller
    {
        private InventarioDesarrolloWebEntities db = new InventarioDesarrolloWebEntities();
        GestorRepository<Producto> data = new GestorRepository<Producto>();
        GestorRepository<GuardarM> da = new GestorRepository<GuardarM>();

        // GET: Productoes
        public async Task <ActionResult> Index()
        {
            //var temoral
            var usuario = Session["usuario"] as Usuario;
            ViewBag.Nombre = usuario.NombreUsuario;

            var producto = await db.Producto
                .Include(p => p.Bodega)
                .Include(p => p.Usuario)
                .ToListAsync();
            return View(producto.ToList());
        }

        // GET: Productoes/Details/5
        public async Task <ActionResult> Details(string id)
        {
            var usuario = Session["usuario"] as Usuario;
            ViewBag.Nombre = usuario.NombreUsuario;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = await data.GetById(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // GET: Productoes/Create
        public ActionResult Create()
        {
            var usuario = Session["usuario"] as Usuario;
            ViewBag.Nombre = usuario.NombreUsuario;
            ViewBag.IdBodega = new SelectList(db.Bodega, "IdBodega", "IdBodega");
            ViewBag.IdUsuario = new SelectList(db.Usuario, "IdUsuario", "IdUsuario");

            return View();

        }

        // POST: Productoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IdProducto,IdBodega,NombreProducto,DescripcionProducto,CostoProducto,PrecioVentaProducto,StockProducto,CategoriaProducto,IdUsuario")] Producto producto)
        {
            var usuario = Session["usuario"] as Usuario;
            ViewBag.Nombre = usuario.NombreUsuario;
            if (ModelState.IsValid)
            {

                producto.IdUsuario = usuario.IdUsuario;
                await data.Create(producto);
                db.SaveChanges();


                var mov = new GuardarM
                {

                    FechaModificacion = DateTime.Now.ToString("dd / MM / yyyy hh: mm:ss tt"),
                    IdUsuario = usuario.IdUsuario,
                    NombreUsuario = usuario.NombreUsuario,
                    IdProducto = producto.IdProducto,
                    NombreProducto = producto.NombreProducto,
                    AccionModificacion = "CREACION PRODUCTOS"
                };
                await da.CreateGuardarM(mov);
                return RedirectToAction("Index");
            }
                ViewBag.IdBodega = new SelectList(db.Bodega, "IdBodega", "NombreBodega", producto.IdBodega);
                ViewBag.IdUsuario = new SelectList(db.Usuario, "IdUsuario", "IdUsuario", producto.IdUsuario);
                return View(producto);
            
        }
                   
            
      

        // GET: Productoes/Edit/5
        public async Task <ActionResult> Edit(string id)
        {
            var usuario = Session["usuario"] as Usuario;
            ViewBag.Nombre = usuario.NombreUsuario;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = await data.GetById(id);
            
            if (producto == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdBodega = new SelectList(db.Bodega, "IdBodega", "IdBodega", producto.IdBodega);
            ViewBag.IdUsuario = new SelectList(db.Usuario, "IdUsuario", "IdUsuario", producto.IdUsuario);
            return View(producto);
        }

        // POST: Productoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <ActionResult> Edit([Bind(Include = "IdProducto,IdBodega,NombreProducto,DescripcionProducto,CostoProducto,PrecioVentaProducto,StockProducto,CategoriaProducto,IdUsuario")] Producto producto)
        {
            var usuario = Session["usuario"] as Usuario;
            ViewBag.Nombre = usuario.NombreUsuario;
            if (ModelState.IsValid)
            {
               producto.IdUsuario = usuario.IdUsuario;
                await data.Update(producto);
                db.SaveChanges();

                var mov = new GuardarM
                {

                    FechaModificacion = DateTime.Now.ToString("dd / MM / yyyy hh: mm:ss tt"),
                    IdUsuario = usuario.IdUsuario,
                    NombreUsuario = usuario.NombreUsuario,
                    IdProducto = producto.IdProducto,
                    NombreProducto = producto.NombreProducto,
                    AccionModificacion = "EDICION PRODUCTOS"
                };
                await da.CreateGuardarM(mov);



                return RedirectToAction("Index");
            }
            ViewBag.IdBodega = new SelectList(db.Bodega, "IdBodega", "IdBodega", producto.IdBodega);
            ViewBag.IdUsuario = new SelectList(db.Usuario, "IdUsuario", "IdUsuario", producto.IdUsuario);
            return View(producto);
        }

        // GET: Productoes/Delete/5
        public async Task <ActionResult> Delete(string id)
        {
            var usuario = Session["usuario"] as Usuario;
            ViewBag.Nombre = usuario.NombreUsuario;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = await data.GetById(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // POST: Productoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task <ActionResult> DeleteConfirmed(string id)
        {
            var usuario = Session["usuario"] as Usuario;
            ViewBag.Nombre = usuario.NombreUsuario;
            Producto producto = await data.GetById(id);
            await data.Delete(producto);
            db.SaveChanges();

            var mov = new GuardarM
            {

                FechaModificacion = DateTime.Now.ToString("dd / MM / yyyy hh: mm:ss tt"),
                IdUsuario = usuario.IdUsuario,
                NombreUsuario = usuario.NombreUsuario,
                IdProducto = producto.IdProducto,
                NombreProducto = producto.NombreProducto,
                AccionModificacion = "ELIMINACION PRODUCTOS"
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
