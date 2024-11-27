using GestorProductoBack.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GestorProductoBack.Repository
{
    public class GuardarT<T> : IGuardarT<T> where T : class
    {
        private InventarioDesarrolloWebEntities _dbContex = new InventarioDesarrolloWebEntities();

        public DbSet<T> dbSet
        {
            get
            {
                return _dbContex.Set<T>();
            }
        }


        public async Task<T> GetById(int id)
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
        public async Task CreateGuardarT(GuardarT mov)
        {
            using (var context = new InventarioDesarrolloWebEntities())
            {
                context.GuardarT.Add(mov);
                await context.SaveChangesAsync();
            }
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }

    }
}