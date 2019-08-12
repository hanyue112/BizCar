using BizCover.Repository.Cars.Dll.Mock.Entities;
using BizCover.Repository.Cars.Dll.Mock.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BizCover.Repository.Cars.Dll.Mock.Classes
{
    public class CarRepository : ICarRepository
    {
        private readonly ConcurrentDictionary<int, Car> mainStoring = new ConcurrentDictionary<int, Car>();

        public CarRepository()
        {
            Add(new Car { Id = CarIDSequence.AcquireID(), Make = "BMW", Model = "X5", Year = 2010, CountryManufactured = "Germany", Colour = "Black", Price = 20500.00M });
            Add(new Car { Id = CarIDSequence.AcquireID(), Make = "Audi", Model = "A6", Year = 1999, CountryManufactured = "Germany", Colour = "Black", Price = 2600.00M });
            Add(new Car { Id = CarIDSequence.AcquireID(), Make = "Benz", Model = "C63", Year = 2009, CountryManufactured = "Germany", Colour = "White", Price = 25500.00M });
            Add(new Car { Id = CarIDSequence.AcquireID(), Make = "Audi", Model = "TT", Year = 2015, CountryManufactured = "Germany", Colour = "Red", Price = 41150.00M });
            Add(new Car { Id = CarIDSequence.AcquireID(), Make = "VW", Model = "Tiguan", Year = 2017, CountryManufactured = "Germany", Colour = "White", Price = 24000.00M });
            Add(new Car { Id = CarIDSequence.AcquireID(), Make = "Benz", Model = "GLE450", Year = 2018, CountryManufactured = "Germany", Colour = "Blue", Price = 70999.00M });
        }

        public Task<int> Add(Car car)
        {
            if (car.Id == 0)
            {
                car.Id = CarIDSequence.AcquireID();
            }

            Car c = mainStoring.AddOrUpdate(car.Id, car, (key, existingCar) =>
              {
                  existingCar.Make = car.Make;
                  existingCar.Model = car.Model;
                  existingCar.Year = car.Year;
                  existingCar.CountryManufactured = car.CountryManufactured;
                  existingCar.Colour = car.Colour;
                  existingCar.Price = car.Price;
                  return existingCar;
              });

            return Task.FromResult(c.Id);
        }

        public Task<List<Car>> GetAllCars()
        {
            Thread.Sleep(1500);//Heavy
            return Task.FromResult(mainStoring.Values.ToArray().ToList());
        }

        public Task Update(Car car)
        {
            if (!mainStoring.ContainsKey(car.Id))
            {
                throw new InvalidOperationException("Key does not exist for update");
            }
            Add(car);
            return Task.CompletedTask;
        }
    }
}