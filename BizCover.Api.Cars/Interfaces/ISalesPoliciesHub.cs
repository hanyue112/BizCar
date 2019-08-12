using BizCover.Api.Cars.Entities;
using BizCover.CarSales.Entities;
using System.Collections.Generic;

namespace BizCover.CarSales.Interfaces
{
    public interface ISalesPoliciesHub
    {
        List<DiscountInfo> ApplyAll(List<BizCoverCar> cars);
    }
}