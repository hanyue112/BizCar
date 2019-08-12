using BizCover.Api.Cars.Entities;
using BizCover.CarSales.Entities;
using BizCover.CarSales.Interfaces;
using System.Collections.Generic;

namespace BizCover.CarSales.SalesPolicies
{
    public class SalesPolicy_Jan00_10 : ISalePolicy
    {
        public List<DiscountInfo> Apply(List<BizCoverCar> cars)
        {
            List<DiscountInfo> ds = new List<DiscountInfo>();
            foreach (BizCoverCar c in cars)
            {
                if (c.Year < 2000)
                {
                    ds.Add(new DiscountInfo { Amount = c.Price * 0.1M, RateInPercent = 1M, Reason = $"this car is built in {c.Year}, before January 2000, discount it by 10% " });
                }
                else
                {
                    ds.Add(new DiscountInfo { Amount = 0M, RateInPercent = 0M, Reason = "this car is built after January 2000, discount it by 0% " });
                }
            }
            return ds;
        }
    }
}