using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.Extensions.Options;
using ShoppingCard.Core.Helpers;
using ShoppingCard.Core.Interfaces;
using ShoppingCard.Core.Models;

namespace ShoppingCard.Core.Operations
{
    public class BasketOperations : IBasketOperations
    {
        //warning exceptionlar ön tarafa taşınacak
        public Settings _settings;
        public static List<BasketModel> _basketList;
        
        public BasketOperations(IOptions<Settings> options)
        {
            _settings = options.Value;
        }

        private BasketModel GetBasket(Guid basketId)
        {
            return _basketList?.FirstOrDefault(a => a.Id == basketId);
        }

        public Guid CreateBasket()
        {
            BasketModel basket = new BasketModel();
            basket.Id = Guid.NewGuid();
            _basketList.Add(basket);
            return basket.Id;
        }
        
        public bool AddProduct(Guid basketId, ProductModel product, int amount)
        {
            var basket = GetBasket(basketId);
            if (basket == null)
            {
                throw new ArgumentNullException("Basket is not found");
            }

            if (product == null || amount <= 0)
            {
                throw new WarningException("The product cannot be blank");
            }
            
            basket.Products.Add(product,amount);
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

        public decimal CalculateDeliveryPrice(Guid basketId)
        {
            var basket = GetBasket(basketId);
            if (basket == null)
            {
                throw new ArgumentNullException("Basket is not found");
            }

            var deliveryCount = BasketHelper.GetDeliveryCount();
            var productCount = BasketHelper.GetProductCount();

            var deliveryPrice = _settings.CalculatorSettings.DeliveryPrice;
            var basketDeliveryPrice = _settings.CalculatorSettings.BasketDeliveryPrice;
            var productDeliveryPrice = _settings.CalculatorSettings.ProductDeliveryPrice;

            var totalDeliveryPrice =
                (deliveryCount * deliveryPrice) + (productCount * productDeliveryPrice) + basketDeliveryPrice;
            return totalDeliveryPrice;
        }

    }
}