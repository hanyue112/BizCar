using BizCover.Api.Cars.Entities;
using BizCover.CarSales.Entities;
using BizCover.CarSales.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace BizCover.CarSales.SalesPolicies
{
    public class SalesPolicy_2Cars_3 : ISalePolicy
    {
        public List<DiscountInfo> Apply(List<BizCoverCar> cars)
        {
            if (cars.Count > 2)
            {
                return new List<DiscountInfo>() { (new DiscountInfo { Amount = cars.Sum(s => s.Price) * 0.03M, RateInPercent = 3M, Reason = $"number of {cars.Count} cars is more than 2, apply 3% discount" }) };
            }
            return new List<DiscountInfo>() { (new DiscountInfo { Amount = 0M, RateInPercent = 0M, Reason = "number of cars is less than 2, apply 0% discount" }) };
        }
    }
}