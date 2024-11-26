using GestorProductoBack.Model;
using GestorProductoBack.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using static GestorProductoBack.Model.GestorModel;

namespace GestorProducto1.Controllers
{
    public class LoginController : Controller
    {


        private InventarioDesarrolloWebEntities db = new InventarioDesarrolloWebEntities();
        GestorRepository<Usuario> data = new GestorRepository<Usuario>();
    
        // GET: Login
        public ActionResult Index(string cedula , string contra)
            {
            //ViewBag.Fecha = DateTime.Now.ToString("dd / MM / yyyy hh: mm:ss tt"); //se  setea la fecha

            if (string.IsNullOrEmpty(cedula) || string.IsNullOrEmpty(contra))
            {
                ViewBag.Error = "El usuario y la contraseña son obligatorios.";
                return View();
            }
            // Buscar al usuario en la base de datos
            var u = db.Usuario.FirstOrDefault(x => x.IdUsuario == cedula);
            if (u != null)
            {
                if(u.IdUsuario == cedula && u.Password == contra)
                {                
                        Session["usuario"] = u;
                    return RedirectToAction("Index", "Home");   
                }
            }
            ViewBag.mensaje = "Usuario o contraseña incorrecto";
           return View();            
        }
    }          
}