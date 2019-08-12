using BizCover.CarSales.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BizCover.Api.Cars.Interfaces
{
    public interface IBizCarRepository
    {
        Task<int> AddAsync(BizCoverCar car);
        Task<bool> UpdateAsync(BizCoverCar car);
        Task<List<BizCoverCar>> GetAllCarsAsync();
    }
}