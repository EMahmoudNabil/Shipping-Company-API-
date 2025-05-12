using ITI.Shipping.Core.Application.Abstraction.Merchant.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Shipping.Core.Application.Abstraction.Merchant;
public interface IMerchantService
{
    Task<IEnumerable<MerchantDTO>> GetMerchantAsync();
}
