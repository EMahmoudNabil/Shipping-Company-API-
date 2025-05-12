using AutoMapper;
using ITI.Shipping.Core.Application.Abstraction.Merchant;
using ITI.Shipping.Core.Application.Abstraction.Merchant.Model;
using ITI.Shipping.Core.Domin.UnitOfWork.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Shipping.Core.Application.Services.MerchantServices;
internal class MerchantService:IMerchantService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public MerchantService(IUnitOfWork unitOfWork , IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<MerchantDTO>> GetMerchantAsync()
    {
       return _mapper.Map<IEnumerable<MerchantDTO>>(await _unitOfWork.GetAllMerchantAsync().GetAllMerchantAsync());
    }
}
