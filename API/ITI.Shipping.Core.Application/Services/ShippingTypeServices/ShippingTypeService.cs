using AutoMapper;
using ITI.Shipping.Core.Application.Abstraction.ShippingType;
using ITI.Shipping.Core.Application.Abstraction.ShippingType.Model;
using ITI.Shipping.Core.Domin.Entities;
using ITI.Shipping.Core.Domin.Pramter_Helper;
using ITI.Shipping.Core.Domin.UnitOfWork.Contract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Shipping.Core.Application.Services.ShippingTypeServices
{
    internal class ShippingTypeService:IShippingTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ShippingTypeService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        // Get All Shipping Type
        public async Task<IEnumerable<ShippingTypeDTO>> GetAllShippingTypeAsync(Pramter pramter)
        {
            return _mapper.Map<IEnumerable<ShippingTypeDTO>>(await _unitOfWork.GetRepository<ShippingType,int>().GetAllAsync(pramter));
        }
        // Get Shipping Type By Id
        public async Task<ShippingTypeDTO> GetShippingTypeAsync(int id)
        {
            return _mapper.Map<ShippingTypeDTO>(await _unitOfWork.GetRepository<ShippingType,int>().GetByIdAsync(id));
        }
        // Add Shipping Type
        public async Task AddAsync(ShippingTypeDTO DTO)
        {
            await _unitOfWork.GetRepository<ShippingType,int>().AddAsync(_mapper.Map<ShippingType>(DTO));
            await _unitOfWork.CompleteAsync();
        }
        // Update Shipping Type
        public async Task UpdateAsync(ShippingTypeDTO DTO)
        {
            var ShippingTypeRepo = _unitOfWork.GetRepository<ShippingType,int>();
            var existingShippingType = await ShippingTypeRepo.GetByIdAsync(DTO.Id);
            if(existingShippingType == null)
                throw new KeyNotFoundException($"ShippingType with ID {DTO.Id} not found.");
            _mapper.Map(DTO,existingShippingType);
            ShippingTypeRepo.UpdateAsync(existingShippingType);
            await _unitOfWork.CompleteAsync();
        }
        // Delete Shipping Type
        public async Task DeleteAsync(int id)
        {
            var ShippingTypeRepo = _unitOfWork.GetRepository<ShippingType,int>();
            var existingShippingType = await ShippingTypeRepo.GetByIdAsync(id);
            if(existingShippingType == null)
                throw new KeyNotFoundException($"ShippingType with ID {id} not found.");
            await ShippingTypeRepo.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
        }
    }
}
