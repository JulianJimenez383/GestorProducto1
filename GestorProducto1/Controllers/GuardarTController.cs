using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Xml.Schema;
using GestorProductoBack.Model;
using GestorProductoBack.Repository;

namespace GestorProducto1.Controllers
{
    public class GuardarTController : Controller
    {

        GestorRepository<Bodega> data = new GestorRepository<Bodega>();
        GestorRepository<GuardarT> da = new GestorRepository<GuardarT>();
        GestorRepository<Producto> pro = new GestorRepository<Producto>();
        private InventarioDesarrolloWebEntities db = new InventarioDesarrolloWebEntities();

        // GET: GuardarT
        public async Task<ActionResult> Index()
        {

            var usuario = Session["usuario"] as Usuario;
            ViewBag.Nombre = usuario.NombreUsuario;
            var guardarT = db.GuardarT.Include(g => g.Producto).Include(g => g.Usuario);
            return View(guardarT.ToList());
        }

        // GET: GuardarT/Details/5
        public async Task<ActionResult> Details(int? id)
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
public async Task<ActionResult> Create()
        {
            var usuario = Session["usuario"] as Usuario;
            ViewBag.Nombre = usuario.NombreUsuario;

            ViewBag.IdProducto = new SelectList(db.Producto, "IdProducto", "IdProducto");
            ViewBag.IdUsuario = new SelectList(db.Usuario, "IdUsuario", "IdUsuario");
            ViewBag.IdBodega = new SelectList(db.Bodega, "IdBodega", "IdBodega");
            return View();
        }

        // POST: GuardarT/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IdTransaccion,IdProducto,CantidadProducto,FechachaTransaccion,IdUsuario,NombreUsuario,ApellidoUsuario,IdBodegaOrigen,IdBodegaDestino")] GuardarT guardarT)
        {
            try
            {
                var usuario = Session["usuario"] as Usuario;
                ViewBag.Nombre = usuario.NombreUsuario;


                if (ModelState.IsValid)
                {
                   

                    
                    if (guardarT.IdBodegaOrigen != guardarT.IdBodegaDestino)
                    {
                        guardarT.IdUsuario = usuario.IdUsuario;
                        guardarT.FechachaTransaccion = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt");
                        guardarT.NombreUsuario = usuario.NombreUsuario;
                        guardarT.ApellidoUsuario = usuario.ApellidoUsuario;

                        //VALIDACION BODEGA ORIGEN
                        var resultado = await db.Producto
                        .Where(p => p.IdProducto == guardarT.IdProducto && p.IdBodega == guardarT.IdBodegaOrigen) // Filtro por los parámetros
                        .Join(db.Bodega,
                        producto => producto.IdBodega, // Clave externa en Productos
                        bodega => bodega.IdBodega,     // Clave primaria en Bodegas
                        (producto, bodega) => new
                        {
                            IdProducto = producto.IdProducto,
                            NombreProducto = producto.NombreProducto,
                            DescripcionProducto = producto.DescripcionProducto,
                            PrecioVentaProducto = producto.PrecioVentaProducto,
                            StockProducto = producto.StockProducto,
                            IdBodega = bodega.IdBodega,
                            NombreBodega = bodega.NombreBodega
                        }).FirstOrDefaultAsync(); // Recuperar el primer resultado que cumpla las condiciones

                        // Verificar si se encontró el resultado
                        if (resultado == null)
                        {
                            ViewBag.ErrorMessage = "BODEGA ORIGEN NO ENCONTRADA";
                            ViewBag.ShowErrorModal = true;
                            return RedirectToAction("Create");
                        }

                        int cantidadP = Convert.ToInt32(guardarT.CantidadProducto);
                        int cantidadbd = Convert.ToInt32(resultado.StockProducto); //convertir stock de bd

                        if (cantidadP > cantidadbd) 
                        {
                            ViewBag.ErrorMessage = "cantidad a tranferir mayor a el stock";
                            ViewBag.ShowErrorModal = true;
                            //ViewBag.Error1 = "cantidad a tranferir mayor a el stock ";

                            return RedirectToAction("Create");
                        }
                        var act = db.Producto.FirstOrDefault(x => x.IdProducto == guardarT.IdProducto);
                        act.StockProducto = cantidadbd - cantidadP;
                        db.SaveChanges();
                        

                        // VALIDACION DE BODEGA DESTINO
                        var resultado2 = await db.Producto
                        .Where(p => p.IdProducto == guardarT.IdProducto+"B" && p.IdBodega == guardarT.IdBodegaDestino) // Filtro por los parámetros
                        .Join(db.Bodega,
                        producto => producto.IdBodega, // Clave externa en Productos
                        bodega => bodega.IdBodega,     // Clave primaria en Bodegas
                        (producto, bodega) => new
                        {
                            IdProducto = producto.IdProducto,
                            NombreProducto = producto.NombreProducto,
                            DescripcionProducto = producto.DescripcionProducto,
                            PrecioVentaProducto = producto.PrecioVentaProducto,
                            StockProducto = producto.StockProducto,
                            IdBodega = bodega.IdBodega,
                            NombreBodega = bodega.NombreBodega
                        }).FirstOrDefaultAsync(); // Recuperar el primer resultado que cumpla las condiciones
                        Console.WriteLine($"IdProducto: {guardarT.IdProducto}, IdBodegaDestino: {guardarT.IdBodegaDestino}");

                        // Verificar si se encontró el resultado
                        if (resultado2 == null)
                        {
                            var actualizado = new Producto
                            {
                                IdProducto = guardarT.IdProducto.ToString() + "B",
                                IdBodega = guardarT.IdBodegaDestino,
                                NombreProducto = resultado.NombreProducto,
                                DescripcionProducto = resultado.DescripcionProducto,
                                CostoProducto = act.CostoProducto,
                                PrecioVentaProducto = act.PrecioVentaProducto,
                                StockProducto = guardarT.CantidadProducto,
                                CategoriaProducto = act.CategoriaProducto,
                                IdUsuario = usuario.IdUsuario



                            };
                            await pro.Create(actualizado);
                            db.SaveChanges();
                        }
                        else
                        {
                            var act2 = db.Producto.FirstOrDefault(x => x.IdProducto == resultado2.IdProducto);
                            act2.StockProducto = resultado2.StockProducto + cantidadP;
                            db.SaveChanges();

                        }
                        await da.Create(guardarT);
                        db.SaveChanges();



                        var mov = new GuardarM
                        {

                            FechaModificacion = DateTime.Now.ToString("dd / MM / yyyy hh: mm:ss tt"),
                            IdUsuario = usuario.IdUsuario,
                            NombreUsuario = usuario.NombreUsuario,
                            IdProducto = guardarT.IdProducto,
                            NombreProducto = guardarT.IdBodegaDestino,
                            AccionModificacion = "TRASLADO BODEGAS"
                        };
                        await da.CreateGuardarM(mov);


                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.Error1 = "Las bodega destino y origen no pueden ser iguales";

                        return RedirectToAction("Create");
                    }

                }


            }
            catch (Exception ex)
            {
                ViewBag.Error = ex;
            }
            return View(guardarT);
        }

        // GET: GuardarT/Edit/5
        public ActionResult Edit(int? id)
        {
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
        public ActionResult Delete(int? id)
        {
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
        public ActionResult DeleteConfirmed(int id)
        {
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
