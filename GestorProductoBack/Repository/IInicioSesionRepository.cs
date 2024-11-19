using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GestorProductoBack.Repository
{
    public interface IInicioSesionRepository<T> : IDisposable where T : class
    {
        Task<T> GetById(string id);



    }
}