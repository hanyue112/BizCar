using BizCover.Repository.Cars.Dll.Mock.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BizCover.Repository.Cars.Dll.Mock.Interfaces
{
    public interface ICarRepository
    {
        Task<int> Add(Car car);
        Task<List<Car>> GetAllCars();
        Task Update(Car car);
    }
}