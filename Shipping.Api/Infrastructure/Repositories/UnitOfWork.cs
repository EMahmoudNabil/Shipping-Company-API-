using Shipping.Api.Core.Abstraction;
using Shipping.Api.Core.Domain.Models;
using Shipping.Api.Infrastructure.Data;
using System.Collections.Concurrent;

namespace Shipping.Api.Infrastructure.Repositories;

public class UnitOfWork:IUnitOfWork
{
    private readonly ShippingContext _context;
    private readonly ConcurrentDictionary<string,object> _repositories;

    private Lazy<IGenericRepository<Order,int>> _orderRepository;

    #region Try Using Lazy Way
    //private readonly Lazy<IGenericRepository<ApplicationUser,string>> _ApplicationUser;
    //private readonly Lazy<IGenericRepository<CitySetting,int>> _CitySetting;
    //private readonly Lazy<IGenericRepository<Region,int>> _Region;
    //private readonly Lazy<IGenericRepository<Branch,int>> _Branch;
    //private readonly Lazy<IGenericRepository<CourierReport,int>> _CourierReport;
    //private readonly Lazy<IGenericRepository<Order,int>> _Order;
    //private readonly Lazy<IGenericRepository<OrderReport,int>> _OrderReport;
    //private readonly Lazy<IGenericRepository<Product,int>> _Product;
    //private readonly Lazy<IGenericRepository<ShippingType,int>> _ShippingType;
    //private readonly Lazy<IGenericRepository<WeightSetting,int>> _WeightSetting; 
    #endregion
    public UnitOfWork(ShippingContext Context)
    {
        _context = Context;
        _repositories = new ConcurrentDictionary<string,object>();

        _orderRepository = new Lazy<IGenericRepository<Order,int>>(() => GetRepository<Order,int>());

        #region Try Using Lazy Way
        //_ApplicationUser = new Lazy<IGenericRepository<ApplicationUser,string>>(() => new GenericRepository<ApplicationUser,string>(_context));
        //_CitySetting = new Lazy<IGenericRepository<CitySetting,int>>(() => new GenericRepository<CitySetting,int>(_context));
        //_Region = new Lazy<IGenericRepository<Region,int>>(() => new GenericRepository<Region,int>(_context));
        //_Branch = new Lazy<IGenericRepository<Branch,int>>(() => new GenericRepository<Branch,int>(_context));
        //_CourierReport = new Lazy<IGenericRepository<CourierReport,int>>(() => new GenericRepository<CourierReport,int>(_context));
        //_Order = new Lazy<IGenericRepository<Order,int>>(() => new GenericRepository<Order,int>(_context));
        //_OrderReport = new Lazy<IGenericRepository<OrderReport,int>>(() => new GenericRepository<OrderReport,int>(_context));
        //_Product = new Lazy<IGenericRepository<Product,int>>(() => new GenericRepository<Product,int>(_context));
        //_ShippingType = new Lazy<IGenericRepository<ShippingType,int>>(() => new GenericRepository<ShippingType,int>(_context));
        //_WeightSetting = new Lazy<IGenericRepository<WeightSetting,int>>(() => new GenericRepository<WeightSetting,int>(_context)); 
        #endregion

    }
    #region Try Using Lazy Way
    //public IGenericRepository<ApplicationUser,string> User => _ApplicationUser.Value;
    //public IGenericRepository<CitySetting,int> City => _CitySetting.Value;
    //public IGenericRepository<Region,int> Region => _Region.Value;
    //public IGenericRepository<Branch,int> Branch => _Branch.Value;
    //public IGenericRepository<CourierReport,int> CourierReport => _CourierReport.Value;
    //public IGenericRepository<Order,int> Order => _Order.Value;
    //public IGenericRepository<OrderReport,int> OrderReport => _OrderReport.Value;
    //public IGenericRepository<Product,int> Product => _Product.Value;
    //public IGenericRepository<ShippingType,int> ShippingType => _ShippingType.Value;
    //public IGenericRepository<WeightSetting,int> WeightSetting => _WeightSetting.Value; 
    #endregion
    public IGenericRepository<Order,int> OrderRepository => GetRepository<Order,int>();


    public IGenericRepository<T,Tkey> GetRepository<T, Tkey>()
        where T : class
        where Tkey : IEquatable<Tkey>
    {
        #region Try Using Dictionary
        //return new GenericRepository<T,Tkey>(_context);
        //var TypeName = typeof(T).Name;
        //if(_repositories.ContainsKey(TypeName))
        //    return (IGenericRepository<T,Tkey>) _repositories[TypeName];
        //var repo = new GenericRepository<T,Tkey>(_context);
        //_repositories.Add(TypeName, repo);
        //return repo; 
        #endregion
        return (IGenericRepository<T,Tkey>) _repositories.GetOrAdd(typeof(T).Name,new GenericRepository<T,Tkey>(_context));
    }
    public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();

    public async ValueTask DisposeAsync() => await _context.DisposeAsync();



}
