using BizCover.Api.Cars.Entities;
using BizCover.CarSales.Entities;
using BizCover.CarSales.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BizCover.CarSales.SalesPoliciesHub
{
    public class SalesPoliciesHub : ISalesPoliciesHub
    {
        private readonly List<ISalePolicy> _policies;

        public SalesPoliciesHub()
        {
            IEnumerable<Type> policiesImps = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.GetInterfaces().Contains(typeof(ISalePolicy)));

            _policies = new List<ISalePolicy>();

            foreach (Type type in policiesImps)
            {
                _policies.Add((ISalePolicy)Activator.CreateInstance(type));
            }
        }

        public List<DiscountInfo> ApplyAll(List<BizCoverCar> cars)
        {
            List<DiscountInfo> discounts = new List<DiscountInfo>();

            if (cars == null)
            {
                return discounts;
            }

            foreach (ISalePolicy i in _policies)
            {
                discounts.AddRange(i.Apply(cars));
            }

            return discounts;
        }
    }
}