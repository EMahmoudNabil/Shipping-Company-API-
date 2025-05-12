using ITI.Shipping.Core.Domin.Entities;
using ITI.Shipping.Core.Domin.Repositories.contract;
using ITI.Shipping.Infrastructure.Presistence.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ITI.Shipping.Infrastructure.Presistence.Repositories;
// This Is WeightSetting Repository Class That Implements The IWeightSettingRepository Interface
public class WeightSettingRepository:GenericRepository<WeightSetting,int>, IWeightSettingRepository
{
    public WeightSettingRepository(ApplicationContext _context) :base(_context)
    {
    }
    public async Task<IEnumerable<WeightSetting>> GetAllWeightSetting()
    {
        return await _context.WeightSettings.ToListAsync();
    }
}