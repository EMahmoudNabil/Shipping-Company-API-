using ITI.Shipping.Core.Domin.Entities;
using ITI.Shipping.Core.Domin.Pramter_Helper;
using ITI.Shipping.Core.Domin.Repositories.contract;
using ITI.Shipping.Infrastructure.Presistence.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ITI.Shipping.Infrastructure.Presistence.Repositories
{
    // This Is A Generic Repository Class That Implements The IGenericRepository Interface
    public class GenericRepository<T , Tkey>:IGenericRepository<T , Tkey> where T : class where Tkey : IEquatable<Tkey>
    {            
        public  ApplicationContext _context;

        public GenericRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<T>> GetAllAsync(Pramter pramter)
        {
            if(pramter.PageSize != null && pramter.PageNumber != null)
            {
                return await _context.Set<T>()
                    .Skip((pramter.PageNumber.Value - 1) * pramter.PageSize.Value)
                    .Take(pramter.PageSize.Value)
                    .ToListAsync();
            }
            else
            {
                return await _context.Set<T>().ToListAsync();
            }

        }
        public async Task<T?> GetByIdAsync(Tkey id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }
        public void UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
        }
        public async Task DeleteAsync(Tkey id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if(entity != null)
            {
                _context.Set<T>().Remove(entity);
            }
        }
    }
}
