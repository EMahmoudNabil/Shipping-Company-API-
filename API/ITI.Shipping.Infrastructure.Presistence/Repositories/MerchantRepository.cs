using ITI.Shipping.Core.Domin.Entities;
using ITI.Shipping.Core.Domin.Entities_Helper;
using ITI.Shipping.Core.Domin.Pramter_Helper;
using ITI.Shipping.Core.Domin.Repositories.contract;
using ITI.Shipping.Infrastructure.Presistence.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Shipping.Infrastructure.Presistence.Repositories;
internal class MerchantRepository:GenericRepository<ApplicationUser,string>, IMerchantRepository
{
    private readonly UserManager<ApplicationUser> _userManager;

    public MerchantRepository(ApplicationContext _Context,UserManager<ApplicationUser> userManager) : base(_Context)
    {
        _userManager = userManager;
    }
    public async Task<IEnumerable<ApplicationUser>> GetAllMerchantAsync()
    {
        return await _userManager.GetUsersInRoleAsync(DefaultRole.Merchant);
    }
}
