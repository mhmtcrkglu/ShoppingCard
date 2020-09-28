using System;
using ShoppingCard.Core.Enums;

namespace ShoppingCard.Core.Models
{
    public class CouponModel
    {
        public Guid Id { get; set; }
        public CouponType Type { get; set; }
        public int Amount { get; set; }
        public decimal MinBasketPrice { get; set; }
    }
}