using System;

namespace ShoppingCard.Core.Interfaces
{
    public interface IBasketOperations
    {
        public decimal CalculateDeliveryPrice(Guid basketId);
    }
}