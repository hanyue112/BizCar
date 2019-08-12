using BizCover.Api.Cars.Entities;
using BizCover.CarSales.Entities;
using System.Collections.Generic;

namespace BizCover.CarSales.Interfaces
{
    public interface ISalePolicy
    {
        List<DiscountInfo> Apply(List<BizCoverCar> cars);
    }
}