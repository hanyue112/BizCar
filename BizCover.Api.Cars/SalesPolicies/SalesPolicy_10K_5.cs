using BizCover.Api.Cars.Entities;
using BizCover.CarSales.Entities;
using BizCover.CarSales.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace BizCover.CarSales.SalesPolicies
{
    public class SalesPolicy_10K_5 : ISalePolicy
    {
        public List<DiscountInfo> Apply(List<BizCoverCar> cars)
        {
            if (cars.Sum(c => c.Price) > 100000)
            {
                return new List<DiscountInfo>() { (new DiscountInfo { Amount = cars.Sum(s => s.Price) * 0.05M, RateInPercent = 5M, Reason = $"total cost {cars.Sum(c => c.Price)} exceeds $100,000 apply 5% discount" }) };
            }
            return new List<DiscountInfo>() { (new DiscountInfo { Amount = 0M, RateInPercent = 0M, Reason = "total cost less than $100,000 apply 0% discount" }) };
        }
    }
}