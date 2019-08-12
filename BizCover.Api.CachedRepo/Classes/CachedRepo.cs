using BizCover.Api.CachedRepo.Entities;
using BizCover.Api.CachedRepo.Interfaces;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BizCover.Repository.Cars.Dll.Mock.Classes;
using BizCover.Repository.Cars.Dll.Mock.Entities;
using System;
using System.Threading;

namespace BizCover.Api.CachedRepo.Classes
{
    public class CachedRepo : ICachedRepo
    {
        private readonly ConcurrentDictionary<int, CachedCar> mainCachedStoring = new ConcurrentDictionary<int, CachedCar>();
        private readonly CarRepository mainStoring = new CarRepository();
        private DateTime CachedDateTime;
        private readonly static object lck = new object();

        public CachedRepo()
        {
            List<Car> repo = mainStoring.GetAllCars().Result;
            foreach (Car c in repo)
            {
                CachedCar cc = new CachedCar(c);

                mainCachedStoring.AddOrUpdate(c.Id, cc, (key, existingCar) =>
                {
                    existingCar.Id = c.Id;
                    existingCar.Make = c.Make;
                    existingCar.Model = c.Model;
                    existingCar.Year = c.Year;
                    existingCar.CountryManufactured = c.CountryManufactured;
                    existingCar.Colour = c.Colour;
                    existingCar.Price = c.Price;
                    return existingCar;
                });
            }
            CachedDateTime = DateTime.Now;
        }

        public async Task<int> AddAsync(CachedCar car)
        {
            Car c = new Car
            {
                Id = car.Id,
                Colour = car.Colour,
                CountryManufactured = car.CountryManufactured,
                Make = car.Make,
                Model = car.Model,
                Price = car.Price,
                Year = car.Year
            };

            int repo_id = await mainStoring.Add(c);
            car.Id = repo_id;

            CachedCar Cc = mainCachedStoring.AddOrUpdate(repo_id, car, (key, existingCar) =>
            {
                existingCar.Make = car.Make;
                existingCar.Model = car.Model;
                existingCar.Year = car.Year;
                existingCar.CountryManufactured = car.CountryManufactured;
                existingCar.Colour = car.Colour;
                existingCar.Price = car.Price;
                return existingCar;
            });

            return await Task.FromResult(repo_id);
        }

        public Task<List<CachedCar>> GetAllCars()
        {
            Task.Run(() => { SyncCacheWithRepo(); });
            return Task.FromResult(mainCachedStoring.Values.ToArray().ToList());
        }

        public async Task UpdateAsync(CachedCar car)
        {
            Car c = new Car
            {
                Id = car.Id,
                Colour = car.Colour,
                CountryManufactured = car.CountryManufactured,
                Make = car.Make,
                Model = car.Model,
                Price = car.Price,
                Year = car.Year
            };

            await mainStoring.Update(c);

            CachedCar Cc = mainCachedStoring.AddOrUpdate(car.Id, car, (key, existingCar) =>
            {
                existingCar.Make = car.Make;
                existingCar.Model = car.Model;
                existingCar.Year = car.Year;
                existingCar.CountryManufactured = car.CountryManufactured;
                existingCar.Colour = car.Colour;
                existingCar.Price = car.Price;
                return existingCar;
            });
        }

        private void SyncCacheWithRepo()
        {
            if (CachedDateTime < DateTime.Now.AddHours(-12))
            {
                if (Monitor.TryEnter(lck, 0))
                {
                    try
                    {
                        List<Car> repo = mainStoring.GetAllCars().Result; //Heavy
                        mainCachedStoring.Clear();

                        foreach (Car c in repo)
                        {
                            CachedCar cc = new CachedCar(c);

                            mainCachedStoring.AddOrUpdate(c.Id, cc, (key, existingCar) =>
                            {
                                existingCar.Id = c.Id;
                                existingCar.Make = c.Make;
                                existingCar.Model = c.Model;
                                existingCar.Year = c.Year;
                                existingCar.CountryManufactured = c.CountryManufactured;
                                existingCar.Colour = c.Colour;
                                existingCar.Price = c.Price;
                                return existingCar;
                            });
                        }
                        CachedDateTime = DateTime.Now;
                    }
                    finally
                    {
                        Monitor.Exit(lck);
                    }
                }
            }
        }
    }
}