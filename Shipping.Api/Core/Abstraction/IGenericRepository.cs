namespace Shipping.Api.Core.Abstraction;

public interface IGenericRepository<T, Tkey> where T : class where Tkey : IEquatable<Tkey>
{
    // This Is A Generic Repository Interface That Contains The Basic CRUD Operations 
    // That Can Be Performed On Any Entity Without ---- User Entity ----     
    Task<List<T>> GetAllAsync();
    Task<T?> GetByIdAsync(Tkey id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}