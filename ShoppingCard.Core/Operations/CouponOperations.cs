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

        public CouponOperations()
        {
        }
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
            Console.WriteLine("Kupon eklenemedi. Mikar, minimum ürün tutarı negatif olamaz.");
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

            return false;
        }
    }
}