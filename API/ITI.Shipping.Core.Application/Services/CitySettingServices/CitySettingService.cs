using AutoMapper;
using ITI.Shipping.Core.Application.Abstraction.Branch.Models;
using ITI.Shipping.Core.Application.Abstraction.CitySetting;
using ITI.Shipping.Core.Application.Abstraction.CitySetting.Models;
using ITI.Shipping.Core.Domin.Entities;
using ITI.Shipping.Core.Domin.Pramter_Helper;
using ITI.Shipping.Core.Domin.UnitOfWork.Contract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Shipping.Core.Application.Services.CitySettingServices
{
    internal class CitySettingService:ICitySettingService
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IMapper _Mapper;

        public CitySettingService(IUnitOfWork UnitOfWork,IMapper mapper)
        {
            _UnitOfWork = UnitOfWork;
            _Mapper = mapper;
        }
        // Get all cities 
        public async Task<IEnumerable<CitySettingDTO>> GetCitySettingsAsync(Pramter pramter)
        {
            return _Mapper.Map<IEnumerable<CitySettingDTO>>(await _UnitOfWork.GetCityRepository().GetAllAsync(pramter));
        }
        // Get city by id
        public async Task<CitySettingDTO> GetCitySettingAsync(int id)
        {
            return _Mapper.Map<CitySettingDTO>(await _UnitOfWork.GetCityRepository().GetByIdAsync(id));
        }
        // Add city
        public async Task AddAsync(CitySettingToAddDTO DTO)
        {
            await _UnitOfWork.GetCityRepository().AddAsync(_Mapper.Map<CitySetting>(DTO));
            await _UnitOfWork.CompleteAsync();
        }
        // Update city
        public async Task UpdateAsync(CitySettingToUpdateDTO DTO)
        {
            var CitySettingRepo=_UnitOfWork.GetCityRepository();
            var existingCitySetting = await CitySettingRepo.GetByIdAsync(DTO.Id);
            if(existingCitySetting == null)
            {
                throw new KeyNotFoundException($"CitySetting with ID {DTO.Id} not found.");
            }
            _Mapper.Map(DTO,existingCitySetting);
            CitySettingRepo.UpdateAsync(existingCitySetting);
            await _UnitOfWork.CompleteAsync();
        }
        // Delete city
        public async Task DeleteAsync(int id)
        {
            var CitySettingRepo = _UnitOfWork.GetCityRepository ();
            var existingCitySetting = await CitySettingRepo.GetByIdAsync(id);
            if(existingCitySetting == null)
            {
                throw new KeyNotFoundException($"CitySetting with ID {id} not found.");
            }
            await CitySettingRepo.DeleteAsync(id);
            await _UnitOfWork.CompleteAsync();
        }
        // Get city by governorate name
        public async Task<IEnumerable<CitySettingDTO>> GetCityByGovernorateName(int regionId)
        {
            var cities = await _UnitOfWork.GetCityRepository().GetCityByGovernorateName(regionId);
            return _Mapper.Map<IEnumerable<CitySettingDTO>>(cities);
        }   
    }
}
