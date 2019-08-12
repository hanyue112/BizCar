using BizCover.Repository.Cars.Dll.Mock.Entities;

namespace BizCover.Api.CachedRepo.Entities
{
    public class CachedCar
    {
        public CachedCar() { }
        public CachedCar(Car c)
        {
            Id = c.Id;
            Make = c.Make;
            Model = c.Model;
            Year = c.Year;
            CountryManufactured = c.CountryManufactured;
            Colour = c.Colour;
            Price = c.Price;
        }

        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string CountryManufactured { get; set; }
        public string Colour { get; set; }
        public decimal Price { get; set; }
    }
}