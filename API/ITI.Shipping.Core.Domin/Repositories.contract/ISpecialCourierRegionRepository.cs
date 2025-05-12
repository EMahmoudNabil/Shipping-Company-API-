using ITI.Shipping.Core.Domin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ITI.Shipping.Core.Domin.Repositories.contract;
// This Is A SpecialCourierRegion Repository Interface That Inharits From The Generic Repository Interface
public interface ISpecialCourierRegionRepository:IGenericRepository<SpecialCourierRegion,int>
{
    // This Method Is Used To Add A Range Of SpecialCourierRegion Entities To The Database
    Task AddRangeAsync(IEnumerable<SpecialCourierRegion> entities);
}
