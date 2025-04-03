using AutoMapper;
using Shipping.Api.Core.Domain.Models;
using Shipping.Api.Infrastructure.Data;
using Shipping.Api.Infrastructure.Dtos;

namespace Shipping.Api.Services;

public class MapperConfig:Profile
{
    public MapperConfig()
    {
        CreateMap<CourierReport,CourierReportDto>().AfterMap((src,dest) =>
        {
            dest.Area = src.Order.CitySetting.Name;
            dest.ClientName = src.Order.MerchantId;
            dest.CustomerName = src.Order.CustomerName;
            dest.CustomerPhone = src.Order.CustomerPhone1;
            dest.CustomerAddress = src.Order.CustomerAddress;
            dest.CourierName = src.Courier.FullName;
            dest.products = src.Order.Products.Select(x => x.Name).ToList();
            dest.orderStatus = src.Order.Status.ToString();
            dest.Amount = src.Order.OrderCost;
            dest.Notes = src.Order.Notes;
        }
        ).ReverseMap();
        CreateMap<Region,RegionDto>().ReverseMap();
        CreateMap<AddEmployeeDTO,ApplicationUser>().AfterMap((src,dest) =>
        {
            dest.UserName = src.Email;
        });
        CreateMap<AddMerchantDTO,ApplicationUser>().AfterMap((src,dest) =>
        {
            dest.UserName = src.Email;
        });
        CreateMap<AddCourierDTO,ApplicationUser>().AfterMap((src,dest) =>
        {
            dest.UserName = src.Email;
        });
        CreateMap<SpecialCityCostDTO,SpecialCityCost>().ReverseMap();
        CreateMap<SpecialCourierRegionDTO,SpecialCourierRegion>().ReverseMap();
        CreateMap<Product,ProductDto>().ReverseMap();

        CreateMap<CitySetting,getCtiyDto>().AfterMap((src,dest) => {

            dest.Govaernorate = src.Region!.Governorate;

        }).ReverseMap();
        CreateMap<CitySetting,addCityDto>().ReverseMap();
        CreateMap<Product, ADDProductDto>().ReverseMap();



        CreateMap<Order,OrderWithProductsDto>().AfterMap((src,dest) =>
        {

            dest.Branch = src.Branch?.Name;
            dest.Region = src.Region?.Governorate;
            dest.City = src.CitySetting?.Name;
            dest.MerchantName = src.MerchantId;
            dest.Status = src.Status.ToString();
            dest.CustomerInfo = $"{src.CustomerName} {src.CustomerPhone1}";

        }).ReverseMap();

        CreateMap<Order,updateOrderDto>().ReverseMap().ForMember(dest => dest.CitySettingId,opt => opt.MapFrom(src => src.City))
        .ForMember(dest => dest.BranchId,opt => opt.MapFrom(src => src.Branch))
        .ForMember(dest => dest.RegionId,opt => opt.MapFrom(src => src.Region))
        .ForMember(dest => dest.ShippingTypeId,opt => opt.MapFrom(src => src.ShippingType))
        .ForMember(dest => dest.PaymentType,opt => opt.MapFrom(src => src.PaymentType))
        .ForMember(dest => dest.MerchantId,opt => opt.MapFrom(src => src.MerchantName))
        .ForMember(dest => dest.Branch,opt => opt.Ignore())
        .ForMember(dest => dest.Region,opt => opt.Ignore())
        .ForMember(dest => dest.ShippingType,opt => opt.Ignore())
        .ForMember(dest => dest.CitySetting,opt => opt.Ignore());
        ;

        CreateMap<Order,addOrderDto>().ReverseMap().ForMember(dest => dest.CitySettingId,opt => opt.MapFrom(src => src.City))
         .ForMember(dest => dest.BranchId,opt => opt.MapFrom(src => src.Branch))
         .ForMember(dest => dest.RegionId,opt => opt.MapFrom(src => src.Region))
         .ForMember(dest => dest.ShippingTypeId,opt => opt.MapFrom(src => src.ShippingType))
         .ForMember(dest => dest.PaymentType,opt => opt.MapFrom(src => src.PaymentType))
         .ForMember(dest => dest.MerchantId,opt => opt.MapFrom(src => src.MerchantName))
         .ForMember(dest => dest.Branch,opt => opt.Ignore())
         .ForMember(dest => dest.Region,opt => opt.Ignore())
         .ForMember(dest => dest.ShippingType,opt => opt.Ignore())
         .ForMember(dest => dest.CitySetting,opt => opt.Ignore());
        ;

      

    } 
   

}



