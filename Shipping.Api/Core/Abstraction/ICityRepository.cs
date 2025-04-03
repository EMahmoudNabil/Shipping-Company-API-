using Shipping.Api.Core.Domain.Models;
using Shipping.Api.Infrastructure.Data;

namespace Shipping.Api.Core.Abstraction;

public interface ICityRepository:IGenericRepository<CitySetting,int>
{
    Task<List<CitySetting>>  getCitybyGovaernorateName(int regoinId);
}
