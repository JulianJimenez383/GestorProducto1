using GestorProductoBack.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GestorProductoBack.Repository
{
    public class GuardarM<T> : IGuardarM<T> where T : class
    {
        private InventarioDesarrolloWebEntities _dbContex = new InventarioDesarrolloWebEntities();

        public DbSet<T> dbSet
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


        public async Task<IEnumerable<T>> getAll()
        {
            return await dbSet.ToListAsync();
        }

        public async Task Create(T entity)
        {
            dbSet.Add(entity);
            await _dbContex.SaveChangesAsync();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }


    }
}