using GestorProductoBack.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GestorProductoBack.Repository
{
    public class GestorRepository<T> : IGestorRepository<T> where T : class
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


        public async Task<IEnumerable<T>> getAll()
        {
            return await dbSet.ToListAsync();
        }

        public async Task Create(T entity)
        {
            dbSet.Add(entity);
            await _dbContex.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            dbSet.AddOrUpdate(entity);
            await _dbContex.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            dbSet.Remove(entity);
            await _dbContex.SaveChangesAsync();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

    }
}