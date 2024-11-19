using GestorProductoBack.Model;
using GestorProductoBack.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestorProducto1.Controllers
{
    public class LoginController : Controller
    {

        private InventarioDesarrolloWebEntities db = new InventarioDesarrolloWebEntities();
        GestorRepository<Usuario> data = new GestorRepository<Usuario>();
        // GET: Login
        public ActionResult Index(string usu , string contra)
        {

            var vista = new Usuario();

            if (usu == null || contra == null) { 
                return View(vista); 
            
            
            }




            return View();
        }
    }
}