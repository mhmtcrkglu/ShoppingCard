using System;
using ShoppingCard.Core.Interfaces;

namespace ShoppingCard.Core
{
    public class BasketManager
    {
        private IBasketOperations _basketOperations;
        
        public BasketManager(IBasketOperations basketOperations)
        {
            _basketOperations = basketOperations;
        }
        public void Run()
        {
            _basketOperations.CalculateDeliveryPrice(Guid.NewGuid());
        }
    }
}