using AutoMapper;
using ITI.Shipping.Core.Application.Abstraction.WeightSetting;
using ITI.Shipping.Core.Application.Abstraction.WeightSetting.Model;
using ITI.Shipping.Core.Domin.Entities;
using ITI.Shipping.Core.Domin.Pramter_Helper;
using ITI.Shipping.Core.Domin.UnitOfWork.Contract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Shipping.Core.Application.Services.WeightSettingServices
{
    internal class WeightSettingService:IWeightSettingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public WeightSettingService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        // Get All WeightSetting
        public async Task<IEnumerable<WeightSettingDTO>> GetAllWeightSettingAsync(Pramter pramter)
        {
            return _mapper.Map<IEnumerable<WeightSettingDTO>>(await _unitOfWork.GetRepository<WeightSetting,int>().GetAllAsync(pramter));
        }
        // Get WeightSetting By Id
        public async Task<WeightSettingDTO> GetWeightSettingAsync(int id)
        {
            return _mapper.Map<WeightSettingDTO>(await _unitOfWork.GetRepository<WeightSetting,int>().GetByIdAsync(id));
        }
        // Add WeightSetting
        public async Task AddAsync(WeightSettingDTO DTO)
        {
            await _unitOfWork.GetRepository<WeightSetting,int>().AddAsync(_mapper.Map<WeightSetting>(DTO));
            await _unitOfWork.CompleteAsync();
        }
        // Update WeightSetting
        public async Task UpdateAsync(WeightSettingDTO DTO)
        {
            var WeightSettingRepo = _unitOfWork.GetRepository<WeightSetting,int>();
            var existingWeightSetting =await WeightSettingRepo.GetByIdAsync(DTO.Id);
            if(existingWeightSetting == null)
                throw new KeyNotFoundException($"WeightSetting with ID {DTO.Id} not found.");
            _mapper.Map(DTO,existingWeightSetting);
            WeightSettingRepo.UpdateAsync(existingWeightSetting);
            await _unitOfWork.CompleteAsync();
        }
        // Delete WeightSetting
        public async Task DeleteAsync(int id)
        {

            var WeightSettingRepo =   _unitOfWork.GetRepository<WeightSetting,int>();
            var existingWeightSetting = await WeightSettingRepo.GetByIdAsync(id);
            if(existingWeightSetting == null)
                throw new KeyNotFoundException($"WeightSetting with ID {id} not found.");
            await WeightSettingRepo.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
        }


    }
}
