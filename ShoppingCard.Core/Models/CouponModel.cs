using System;

namespace ShoppingCard.Core.Models
{
    public class CouponModel
    {
        public Guid Id { get; set; }
        public decimal MinBasketPrice { get; set; }
    }
}