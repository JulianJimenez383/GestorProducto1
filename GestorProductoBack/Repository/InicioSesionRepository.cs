using GestorProductoBack.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GestorProductoBack.Repository
{

    public class InicioSesionRepository<T> : IInicioSesionRepository<T> where T : class
    {
        private InventarioDesarrolloWebEntities _dbContex = new InventarioDesarrolloWebEntities();

        private DbSet<T> dbSet
        {
            get
            {
                return _dbContex.Set<T>();
            }
        }

        public async Task<T> GetById(string id)
        {
            return await dbSet.FindAsync(id);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }


    }
}