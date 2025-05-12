using ITI.Shipping.Core.Domin.Entities;
using ITI.Shipping.Core.Domin.Repositories.contract;
using ITI.Shipping.Core.Domin.UnitOfWork.Contract;
using ITI.Shipping.Infrastructure.Presistence.Data;
using ITI.Shipping.Infrastructure.Presistence.Repositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ITI.Shipping.Infrastructure.Presistence.UnitOfWork
{
    // This Is A UnitOfWork Class That Implements The IUnitOfWork Interface
    public class UnitOfWork:IUnitOfWork
    {
        private readonly ApplicationContext _context;
        private readonly ConcurrentDictionary<string, object> _repositories;
        private readonly UserManager<ApplicationUser> _userManager;
        public UnitOfWork(ApplicationContext Context,UserManager<ApplicationUser> userManager)
        {
            _context = Context;
            _repositories = new ConcurrentDictionary<string,object>();
            _userManager = userManager;

        }
        // This Method Is Used To Get A Generic Repository For A Specific Entity Type
        public IGenericRepository<T,Tkey> GetRepository<T, Tkey>()
            where T : class
            where Tkey : IEquatable<Tkey>
        {
            // Check If The Repository Already Exists In The Dictionary Or Not
            return (IGenericRepository<T,Tkey>) _repositories.GetOrAdd(typeof(T).Name,new GenericRepository<T,Tkey>(_context));
        }
        public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();
        public async ValueTask DisposeAsync() => await _context.DisposeAsync();
        // This Method Is Used To Get The City Repository
        public ICityRepository GetCityRepository()
        {
            // Check If The Repository Already Exists In The Dictionary Or Not
            return (ICityRepository) _repositories.GetOrAdd(typeof(CitySetting).Name,new CityRepository(_context));
        }
        // This Method Is Used To Get The Special Courier Region Repository
        public ISpecialCourierRegionRepository GetSpecialCourierRegionRepository()
        {
            // Check If The Repository Already Exists In The Dictionary Or Not
            return (ISpecialCourierRegionRepository) _repositories.GetOrAdd(typeof(SpecialCourierRegion).Name,new SpecialCourierRegionRepository(_context));
        }
        // This Method Is Used To Get The Special City Cost Repository
        public ISpecialCityCostRepository GetSpecialCityCostRepository()
        {
            // Check If The Repository Already Exists In The Dictionary Or Not
            return (ISpecialCityCostRepository) _repositories.GetOrAdd(typeof(SpecialCityCost).Name,new SpecialCityCostRepository(_context));
        }
        // This Method Is Used To Get The Order Repository
        public IOrderRepository GetOrderRepository()
        {
            // Check If The Repository Already Exists In The Dictionary Or Not
            return (IOrderRepository) _repositories.GetOrAdd(typeof(Order).Name,new OrderRepository(_context));
        }
        // This Method Is Used To Get The Weight Setting Repository
        public IWeightSettingRepository GetWeightSettingRepository()
        {
            // Check If The Repository Already Exists In The Dictionary Or Not
            return (IWeightSettingRepository) _repositories.GetOrAdd(typeof(WeightSetting).Name,new WeightSettingRepository(_context));
        }
        // This Method Is Used To Get The Employee Repository
        public IEmployeeRepository GetAllEmployeesAsync()
        {
            // Check If The Repository Already Exists In The Dictionary Or Not
            return (IEmployeeRepository) _repositories.GetOrAdd(typeof(ApplicationUser).Name,new EmployeeRepository(_context,_userManager));
        }
        // This Method Is Used To Get The Order Report Repository
        public IOrderReportRepository GetOrderReportRepository()
        {
            // Check If The Repository Already Exists In The Dictionary Or Not
            return (IOrderReportRepository) _repositories.GetOrAdd(typeof(OrderReport).Name,new OrderReportRepository(_context));
        }
        // This Method Is Used To Get The Merchant Repository
        public IMerchantRepository GetAllMerchantAsync()
        {
            return (IMerchantRepository) _repositories.GetOrAdd(typeof(ApplicationUser).Name,new MerchantRepository(_context,_userManager));
        }
    }
}