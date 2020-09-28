using System;
using ShoppingCard.Core.Models;

namespace ShoppingCard.Core.Interfaces
{
    public interface IBasketOperations
    {
        public BasketModel GetBasket(Guid basketId);
        public Guid CreateBasket();
        public bool BindProduct(Guid basketId, ProductModel product, int amount);
        public bool RemoveProduct(Guid basketId, Guid productId, int amount);
        public decimal CalculateTotalDeliveryPrice(BasketModel basket);
        public bool ApplyCampaignToBasket(Guid basketId, CampaignModel coupon);
        public bool ApplyCouponToBasket(Guid basketId, CouponModel campaign);
        public BasketModel CalculateBasket(Guid basketId);
    }
}