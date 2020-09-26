using System;
using System.Collections.Generic;
using System.Linq;
using ShoppingCard.Core.Interfaces;
using ShoppingCard.Core.Models;

namespace ShoppingCard.Core.Operations
{
    public class CouponOperations : ICouponOperations
    {
        public static List<CouponModel> _couponList;

        public CouponOperations()
        {
        }

        public CouponModel GetCoupon(Guid couponId)
        {
            return _couponList.FirstOrDefault(a => a.Id == couponId);
        }

        public Guid AddCoupon(decimal minBasketPrice)
        {
            var coupon = new CouponModel
            {
                Id = Guid.NewGuid(),
                MinBasketPrice = minBasketPrice
            };
            _couponList.Add(coupon);

            return coupon.Id;
        }

        public bool RemoveCoupon(Guid couponId)
        {
            var coupon = GetCoupon(couponId);

            if (coupon != null)
            {
                _couponList.Remove(coupon);
                return true;
            }

            return false;
        }
    }
}