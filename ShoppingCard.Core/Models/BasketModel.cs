using System;
using System.Collections.Generic;

namespace ShoppingCard.Core.Models
{
    public class BasketModel
    {
        public Guid Id { get; set; }
        public Dictionary<ProductModel,int> Products { get; set; }
        public decimal BasketTotal { get; set; }
        public decimal DeliveryPrice { get; set; }
        public CouponModel Coupon { get; set; }
        public CampaignModel Campaign { get; set; }
    }
}