using System;
using System.Collections.Generic;
using System.Linq;
using ShoppingCard.Core.Enums;
using ShoppingCard.Core.Interfaces;
using ShoppingCard.Core.Models;

namespace ShoppingCard.Core.Operations
{
    public class CouponOperations : ICouponOperations
    {
        public static readonly List<CouponModel> CouponList = new List<CouponModel>();
        
        public CouponModel GetCoupon(Guid couponId)
        {
            return CouponList.FirstOrDefault(a => a.Id == couponId);
        }
        public CouponModel AddCoupon(CouponType type, int amount,decimal minBasketPrice)
        {
            if (amount >= 0 && minBasketPrice >= 0)
            {
                var coupon = new CouponModel
                {
                    Id = Guid.NewGuid(),
                    Type = type,
                    Amount = amount,
                    MinBasketPrice = minBasketPrice
                };
                CouponList.Add(coupon);
                
                return coupon;
            }
            Console.WriteLine("Coupon could not be added. Coupon amount, minimum product price cannot be negative");

            return null;

        }
        public bool RemoveCoupon(Guid couponId)
        {
            var coupon = GetCoupon(couponId);

            if (coupon != null)
            {
                CouponList.Remove(coupon);
                return true;
            }
            Console.WriteLine("Coupon is not found");
            return false;
        }
    }
}