using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.Extensions.Options;
using ShoppingCard.Core.Enums;
using ShoppingCard.Core.Helpers;
using ShoppingCard.Core.Interfaces;
using ShoppingCard.Core.Models;

namespace ShoppingCard.Core.Operations
{
    public class BasketOperations : IBasketOperations
    {
        private readonly Settings _settings;
        private static readonly List<BasketModel> BasketList = new List<BasketModel>();

        public BasketOperations(IOptions<Settings> options)
        {
            _settings = options.Value;
        }

        public BasketModel GetBasket(Guid basketId)
        {
            return BasketList?.FirstOrDefault(a => a.Id == basketId);
        }
        public Guid CreateBasket()
        {
            BasketModel basket = new BasketModel();
            basket.Id = Guid.NewGuid();
            BasketList.Add(basket);
            return basket.Id;
        }
        public bool BindProduct(Guid basketId, ProductModel product, int amount)
        {
            var basket = GetBasket(basketId);
            if (basket == null)
            {
                throw new ArgumentNullException("Basket is not found");
            }

            if (amount <= 0)
            {
                Console.WriteLine("{0} cannot be added. Product quantity must be greater than 0", product.Title);
                return false;
            }

            basket.Products.Add(product, amount);
            basket.SubTotal = BasketHelper.GetSubTotal(basket);
            basket.DeliveryPrice += CalculateTotalDeliveryPrice(basket);

            return true;
        }
        public bool RemoveProduct(Guid basketId, Guid productId, int amount)
        {
            var basket = GetBasket(basketId);
            if (basket == null)
            {
                throw new ArgumentNullException("Basket is not found");
            }

            var product = basket.Products?.FirstOrDefault(a => a.Key.Id == productId);

            if (product.HasValue)
            {
                if (amount < product.Value.Value)
                {
                    basket.Products[product.Value.Key] = product.Value.Value - amount;
                }

                basket.Products.Keys.ToList().Remove(product.Value.Key);
            }

            return false;
        }
        public decimal CalculateTotalDeliveryPrice(BasketModel basket)
        {
            var deliveryPrice = _settings.CalculatorSettings.DeliveryPrice;
            var basketDeliveryPrice = _settings.CalculatorSettings.BasketDeliveryPrice;
            var productDeliveryPrice = _settings.CalculatorSettings.ProductDeliveryPrice;

            var totalDelivery =
                BasketHelper.CalculateDeliveryPrice(basket, deliveryPrice, basketDeliveryPrice, productDeliveryPrice);

            return totalDelivery;
        }
        public bool ApplyCampaignToBasket(Guid basketId, CampaignModel campaign)
        {
            var basket = GetBasket(basketId);
            if (basket == null)
            {
                throw new ArgumentNullException("Basket is not found");
            }

            if (campaign != null && basket.Products.Count >= campaign.MinimumProductCount)
            {
                var affectedProductIds = BasketHelper.CheckCampaignInBasket(basket, campaign);
                if (affectedProductIds.Any(a=>a == Guid.Empty) || affectedProductIds.Count <= 0)
                {
                    Console.WriteLine("{0} could not be applied",campaign.Title);
                }
               
                foreach (var product in basket.Products)
                {
                    var checkProduct = affectedProductIds.Any(a => a == product.Key.Id);
                    if (checkProduct && product.Key != null && product.Value > 0)
                    {
                        decimal campaignDiscount = decimal.Zero;
                        switch (campaign.Type)
                        {
                            case CampaignType.Rate:
                                campaignDiscount = (((product.Key.Price) * campaign.Amount) / 100) *
                                                   product.Value;
                                break;
                            case CampaignType.Total:
                                campaignDiscount = campaign.Amount * product.Value;
                                break;
                        }

                        basket.DiscountTotal += campaignDiscount;
                    }
                }
            }
            basket.Campaigns.Add(campaign);

            return true;
        }
        public bool ApplyCouponToBasket(Guid basketId, CouponModel coupon)
        {
            var basket = GetBasket(basketId);
            if (basket == null)
            {
                throw new ArgumentNullException("Basket is not found");
            }

            if (coupon != null && basket.SubTotal >= coupon.MinBasketPrice)
            {
                decimal couponDiscount = decimal.Zero;
                switch (coupon.Type)
                {
                    case CouponType.Rate:
                        couponDiscount = (basket.SubTotal * coupon.Amount) / 100;
                        break;
                    case CouponType.Total:
                        couponDiscount = coupon.Amount;
                        break;
                }

                basket.CouponTotal += couponDiscount;
                
                basket.Coupons.Add(coupon);

                return true;
            }
            Console.WriteLine("coupon could not be applied to basket.");
            return false;
        }
        public BasketModel CalculateBasket(Guid basketId)
        {
            var basket = GetBasket(basketId);
            if (basket == null)
            {
                throw new ArgumentNullException("Basket is not found");
            }

            basket.BasketTotal =
                basket.SubTotal + basket.DeliveryPrice - basket.DiscountTotal - basket.CouponTotal;

            return basket;
        }
    }
}