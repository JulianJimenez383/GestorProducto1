using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestorProductoBack.Model
{
    public class GestorModel
    {

        public IEnumerable<Usuario> usuarios { get; set; }

        public IEnumerable<Producto> productos { get; set; }

        public IEnumerable<Proveedor> proveedor { get; set; }

        public IEnumerable<Proveedor> GuardarM { get; set; }

        public class MensajeViewModel
        {
            public string Mensaje { get; set; }
        }


    }
}