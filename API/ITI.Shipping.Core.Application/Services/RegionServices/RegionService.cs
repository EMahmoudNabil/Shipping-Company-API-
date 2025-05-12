using AutoMapper;
using ITI.Shipping.Core.Application.Abstraction.Region;
using ITI.Shipping.Core.Application.Abstraction.Region.Model;
using ITI.Shipping.Core.Domin.Entities;
using ITI.Shipping.Core.Domin.Pramter_Helper;
using ITI.Shipping.Core.Domin.UnitOfWork.Contract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Shipping.Core.Application.Services.RegionServices
{
    internal class RegionService:IRegionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RegionService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        // Get All Regions
        public async Task<IEnumerable<RegionDto>> GetRegionsAsync(Pramter pramter)
        {
           return  _mapper.Map<IEnumerable<RegionDto>>(await _unitOfWork.GetRepository<Region,int>().GetAllAsync(pramter));
        }
        // Get Region By Id
        public async Task<RegionDto> GetRegionAsync(int id)
        {
            return _mapper.Map<RegionDto>(await _unitOfWork.GetRepository<Region,int>().GetByIdAsync(id));
        }
        // Add Region
        public Task AddAsync(RegionDto DTO)
        {
            _unitOfWork.GetRepository<Region,int>().AddAsync(_mapper.Map<Region>(DTO));
            return _unitOfWork.CompleteAsync();
        }
        // Update Region
        public async Task UpdateAsync(RegionDto DTO)
        {
            var regionRepo = _unitOfWork.GetRepository<Region,int>();
            var existingregion = await regionRepo.GetByIdAsync(DTO.Id);
            if(existingregion == null)
                throw new KeyNotFoundException($"Region with ID {DTO.Id} not found.");

             _mapper.Map(DTO,existingregion);
             regionRepo.UpdateAsync( existingregion);
             await _unitOfWork.CompleteAsync();
        }
        // Delete Region
        public async Task DeleteAsync(int id)
        {
            var regionRepo = _unitOfWork.GetRepository<Region,int>();
            var existingregion= regionRepo.GetByIdAsync(id);
            if(existingregion==null)
                throw new KeyNotFoundException($"Region with ID {id} not found.");
            await regionRepo.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
        }
    }
}
