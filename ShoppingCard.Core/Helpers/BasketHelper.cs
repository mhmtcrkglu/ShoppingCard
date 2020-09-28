using System;
using System.Collections.Generic;
using System.Linq;
using ShoppingCard.Core.Models;

namespace ShoppingCard.Core.Helpers
{
    public static class BasketHelper
    {
        public static decimal CalculateDeliveryPrice(BasketModel basket,decimal deliveryPrice, decimal basketDeliveryPrice, decimal productDeliveryPrice)
        {
            var deliveryCount = GetDeliveryCount(basket);
            var productCount = GetProductCount(basket);
            
            var totalDeliveryPrice =
                (deliveryCount * deliveryPrice) + (productCount * productDeliveryPrice) + basketDeliveryPrice;
            return totalDeliveryPrice;
        }
        public static decimal GetSubTotal(BasketModel basket)
        {
            return basket.Products.Sum(a => a.Key.Price * a.Value);
        }
        public static int GetDeliveryCount(BasketModel basket)
        {
            return basket.Products.GroupBy(a => a.Key.Category.Id).Count();
        }
        public static int GetProductCount(BasketModel basket)
        {
            return basket.Products.GroupBy(a => a.Key.Id).Count();
        }

        public static List<Guid> CheckCampaignInBasket(BasketModel basket, CampaignModel campaign)
        {
            var products = basket.Products?.Select(a=>a.Key).ToList();
            var commonCategory = products?.Select(a=>a.Category.Id).Intersect(campaign.CategoryIds).FirstOrDefault();
            var affectedProducts = products?.Where(a => a.Category.Id == commonCategory).Select(a=>a.Id).ToList();
            return affectedProducts;
        }
    }
}