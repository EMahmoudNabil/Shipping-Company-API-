using ITI.Shipping.Core.Domin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ITI.Shipping.Core.Domin.Repositories.contract;
// This Is A City Repository Interface That Inherits From The Generic Repository Interface
public interface ICityRepository:IGenericRepository<CitySetting,int>
{
    // This Method Is Used To Get All Cities By Region Id
    Task<IEnumerable<CitySetting>> GetCityByGovernorateName(int regionId);   
}
