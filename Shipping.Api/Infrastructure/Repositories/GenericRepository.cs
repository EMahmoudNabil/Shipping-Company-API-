using Microsoft.EntityFrameworkCore;
using Shipping.Api.Core.Abstraction;
using Shipping.Api.Infrastructure.Data;
namespace Shipping.Api.Infrastructure.Repositories;

    public class GenericRepository<T, Tkey>:IGenericRepository<T,Tkey> where T : class where Tkey : IEquatable<Tkey>
    {


        public ShippingContext _context;

        public GenericRepository(ShippingContext context)
        {
            _context = context;
        }
        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
        public async Task<T?> GetByIdAsync(Tkey id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        public async Task AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateAsync(T entity)
    {
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(T entity)
    {
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
    }
    public async Task Save() { 

        await _context.SaveChangesAsync();

    }


    // This Is A Generic Repository  That Contains The Basic CRUD Operations 
    // That Can Be Performed On Any Entity Without ---- User Entity ----  


    //public async Task<ApplicationUser?> GetUserByIdAsync(string id)
    //{
    //    return await _context.Set<ApplicationUser>().FirstOrDefaultAsync(user => user.Id == id);
    //}
}

