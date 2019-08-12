using BizCover.Api.CachedRepo.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BizCover.Api.CachedRepo.Interfaces
{
    public interface ICachedRepo
    {
        Task<int> AddAsync(CachedCar car);
        Task UpdateAsync(CachedCar car);
        Task<List<CachedCar>> GetAllCars();
    }
}