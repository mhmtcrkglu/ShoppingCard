using System;
using System.Collections.Generic;

namespace ShoppingCard.Core.Models
{
    public class BasketModel
    {
        public Guid Id { get; set; }
        public Dictionary<ProductModel,int> Products { get; set; } = new Dictionary<ProductModel, int>();
        public decimal SubTotal { get; set; }
        public decimal DiscountTotal { get; set; }
        public decimal CouponTotal { get; set; }
        public decimal BasketTotal { get; set; }
        public decimal DeliveryPrice { get; set; }
        public List<CouponModel> Coupons { get; set; } = new List<CouponModel>();
        public List<CampaignModel> Campaigns { get; set; } = new List<CampaignModel>();
    }
}