using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GestorProductoBack.Repository
{
    public interface IGuardarM<T> : IDisposable where T : class
    {
        Task<T> GetById(string id);
        Task<IEnumerable<T>> getAll();
        Task Create(T entity);
  
    }
}