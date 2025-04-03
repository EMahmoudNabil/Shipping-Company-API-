namespace Shipping.Api.Core.Abstraction;

public interface IUnitOfWork:IAsyncDisposable
{
    #region Try Using Lazy Way
    //public IGenericRepository<ApplicationUser , string> User { get; }
    //public IGenericRepository<CitySetting,int> City { get; }
    //public IGenericRepository<Region,int> Region { get; }
    //public IGenericRepository<Branch,int> Branch { get; }
    //public IGenericRepository<CourierReport,int> CourierReport { get; }
    //public IGenericRepository<Order,int> Order { get; }
    //public IGenericRepository<OrderReport,int> OrderReport { get; }
    //public IGenericRepository<Product,int> Product { get; }
    //public IGenericRepository<ShippingType,int> ShippingType { get; }
    //public IGenericRepository<WeightSetting,int> WeightSetting { get; } 
    #endregion
    IGenericRepository<T,Tkey> GetRepository<T, Tkey>()
        where T : class where Tkey : IEquatable<Tkey>;
    Task<int> CompleteAsync();
}