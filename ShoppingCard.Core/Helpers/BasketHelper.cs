using System;
using System.Collections.Generic;
using System.Linq;
using ShoppingCard.Core.Models;

namespace ShoppingCard.Core.Helpers
{
    public static class BasketHelper
    {
        public static decimal CalculateDeliveryPrice(BasketModel basket, decimal deliveryPrice,
            decimal basketDeliveryPrice, decimal productDeliveryPrice)
        {
            var deliveryCount = GetDeliveryCount(basket); //total different category count
            var productCount = GetProductCount(basket); //total different product count 

            var totalDeliveryPrice =
                (deliveryCount * deliveryPrice) + (productCount * productDeliveryPrice) + basketDeliveryPrice;
            return totalDeliveryPrice;
        }

        public static decimal GetSubTotal(BasketModel basket)
        {
            return basket.Products.Sum(a => a.Key.Price * a.Value);
        }

        private static int GetDeliveryCount(BasketModel basket)
        {
            return basket.Products.GroupBy(a => a.Key.Category.Id).Count(); //Per category
        }

        private static int GetProductCount(BasketModel basket)
        {
            return basket.Products.GroupBy(a => a.Key.Id).Count(); //Per product
        }

        public static List<Guid> CheckCampaignInBasket(BasketModel basket, CampaignModel campaign)
        {
            var affectedProducts = new List<Guid>();
            var products = basket.Products?.Select(a => a.Key).ToList();

            if (campaign.CategoryIds != null && campaign.CategoryIds.Count > 0)
            {
                var commonCategory = products?.Select(a => a.Category.Id).Intersect(campaign.CategoryIds)
                    .FirstOrDefault();
                if (commonCategory != null)
                {
                    affectedProducts = products?.Where(a => a.Category.Id == commonCategory).Select(a => a.Id).ToList();
                    return affectedProducts;
                }
            }

            return affectedProducts;
        }
    }
}