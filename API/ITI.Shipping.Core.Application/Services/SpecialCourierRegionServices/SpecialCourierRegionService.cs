using AutoMapper;
using ITI.Shipping.Core.Application.Abstraction.SpecialCourierRegion;
using ITI.Shipping.Core.Application.Abstraction.SpecialCourierRegion.Model;
using ITI.Shipping.Core.Domin.Entities;
using ITI.Shipping.Core.Domin.Pramter_Helper;
using ITI.Shipping.Core.Domin.UnitOfWork.Contract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Shipping.Core.Application.Services.SpecialCourierRegionServices
{
    internal class SpecialCourierRegionService:ISpecialCourierRegionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SpecialCourierRegionService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        // Get All SpecialCourierRegions
        public async Task<IEnumerable<SpecialCourierRegionDTO>> GetSpecialCourierRegionsAsync(Pramter pramter)
        {
            return  _mapper.Map<IEnumerable<SpecialCourierRegionDTO>>(await _unitOfWork.GetRepository<SpecialCourierRegion,int>().GetAllAsync(pramter));
        }
        // Get SpecialCourierRegion By Id
        public async Task<SpecialCourierRegionDTO> GetSpecialCourierRegionAsync(int id)
        {
            return _mapper.Map<SpecialCourierRegionDTO>(await _unitOfWork.GetRepository<SpecialCourierRegion,int>().GetByIdAsync(id));
        }
        // Add SpecialCourierRegion
        public async Task AddAsync(SpecialCourierRegionDTO DTO)
        {
            await _unitOfWork.GetRepository<SpecialCourierRegion,int>().AddAsync(_mapper.Map<SpecialCourierRegion>(DTO));
            await _unitOfWork.CompleteAsync();
        }
        // Update SpecialCourierRegion
        public async Task UpdateAsync(SpecialCourierRegionDTO DTO)
        {
            var SpecialCourierRegionRepo = _unitOfWork.GetRepository<SpecialCourierRegion,int>();
            var existingSpecialCourierRegion =await SpecialCourierRegionRepo.GetByIdAsync(DTO.Id);
            if (existingSpecialCourierRegion == null)
                throw new KeyNotFoundException($"SpecialCourierRegion with ID {DTO.Id} not found.");
            _mapper.Map(DTO,existingSpecialCourierRegion);
            SpecialCourierRegionRepo.UpdateAsync(existingSpecialCourierRegion);
            await _unitOfWork.CompleteAsync();
        }
        // Delete SpecialCourierRegion
        public async Task DeleteAsync(int id)
        {
            var SpecialCourierRegionRepo = _unitOfWork.GetRepository<SpecialCourierRegion,int>();
            var existingSpecialCourierRegion =  SpecialCourierRegionRepo.GetByIdAsync(id);
            if(existingSpecialCourierRegion == null)
                throw new KeyNotFoundException($"SpecialCourierRegion with ID {id} not found.");
            await SpecialCourierRegionRepo.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
        }


    }
}
