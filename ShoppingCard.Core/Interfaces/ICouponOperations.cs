using System;
using ShoppingCard.Core.Enums;
using ShoppingCard.Core.Models;

namespace ShoppingCard.Core.Interfaces
{
    public interface ICouponOperations
    {
        public CouponModel GetCoupon(Guid couponId);
        public CouponModel AddCoupon(CouponType type, int amount,decimal minBasketPrice);
        public bool RemoveCoupon(Guid couponId);
    }
}