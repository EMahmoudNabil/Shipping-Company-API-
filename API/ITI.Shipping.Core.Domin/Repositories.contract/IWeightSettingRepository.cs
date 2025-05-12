using ITI.Shipping.Core.Domin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ITI.Shipping.Core.Domin.Repositories.contract;
// This Is WeightSetting Repository Interface That Inharits From The Generic Repository Interface
public interface IWeightSettingRepository:IGenericRepository<WeightSetting,int>
{
    // This Method To Get All WeightSetting
    Task<IEnumerable<WeightSetting>> GetAllWeightSetting();
}
