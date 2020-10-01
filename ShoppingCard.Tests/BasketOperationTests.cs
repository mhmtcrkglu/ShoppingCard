using System;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using ShoppingCard.Core;
using ShoppingCard.Core.Enums;
using ShoppingCard.Core.Models;
using ShoppingCard.Core.Operations;
using Xunit;

namespace ShoppingCard.Tests
{
    public class BasketOperationTests
    {
        private readonly BasketOperations _basketOperations = new BasketOperations(Options.Create(
            new Settings()
            {
                CalculatorSettings = new CalculatorSettings()
                {
                    DeliveryPrice = Convert.ToDecimal(1.2),
                    BasketDeliveryPrice = Convert.ToDecimal(2.0),
                    ProductDeliveryPrice = Convert.ToDecimal(1.0)
                }
            }));

        [Fact]
        public void GetBasketById_ShouldReturnBasket_WhenBasketExists()
        {
            //Arrange
            var basketId = Guid.NewGuid();
            var basketDto = new BasketModel
            {
                Id = basketId
            };
            BasketOperations.BasketList.Add(basketDto);

            //Act
            var basket = _basketOperations.GetBasket(basketId);

            //Assert
            Assert.Equal(basketId, basket.Id);
        }

        [Fact]
        public void GetBasketById_ShouldReturnBasket_WhenBasketDoesNotExists()
        {
            //Arrange
            var basketId = Guid.NewGuid();
            var basketDto = new BasketModel
            {
                Id = basketId
            };
            BasketOperations.BasketList.Add(basketDto);

            //Act
            var basket = _basketOperations.GetBasket(Guid.NewGuid());

            //Assert
            Assert.Null(basket);
        }

        [Fact]
        public void AddBasket_ShouldReturnBasketId_WhenBasketAddedSuccess()
        {
            //Act
            var result = _basketOperations.AddBasket();

            //Assert
            Assert.NotEqual(Guid.Empty, result);
        }

        [Fact]
        public void BindProduct_ShouldReturnTrue_WhenBindToBasket()
        {
            //Arrange
            var basketId = Guid.NewGuid();
            var basketDto = new BasketModel
            {
                Id = basketId
            };
            BasketOperations.BasketList.Add(basketDto);

            var categoryId = Guid.NewGuid();
            var categoryTitle = "Technology";
            var categoryDto = new CategoryModel
            {
                Id = categoryId,
                Title = categoryTitle
            };

            var productId = Guid.NewGuid();
            var productTitle = "Mobile Phone";
            var productDto = new ProductModel
            {
                Id = productId,
                Title = productTitle,
                Category = categoryDto,
                Price = 4900
            };

            var amount = 1;

            //Act
            var result = _basketOperations.BindProduct(basketDto.Id, productDto, amount);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void BindProduct_ShouldReturnFalse_WhenNotBindToBasket()
        {
            //Arrange
            var basketId = Guid.NewGuid();
            var basketDto = new BasketModel
            {
                Id = basketId
            };
            BasketOperations.BasketList.Add(basketDto);

            var productId = Guid.NewGuid();
            var productTitle = "Mobile Phone";
            var productDto = new ProductModel
            {
                Id = productId,
                Title = productTitle
            };

            var amount = -1;

            //Act
            var result = _basketOperations.BindProduct(basketDto.Id, productDto, amount);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void RemoveProduct_ShouldReturnTrue_WhenProductRemovedFromBasket()
        {
            //Arrange
            var productId = Guid.NewGuid();
            var productTitle = "Mobile Phone";
            var productAmount = 2;
            Dictionary<ProductModel, int> productDictionary = new Dictionary<ProductModel, int>();
            var productDto = new ProductModel
            {
                Id = productId,
                Title = productTitle
            };
            productDictionary.Add(productDto, productAmount);

            var basketId = Guid.NewGuid();
            var basketDto = new BasketModel
            {
                Id = basketId,
                Products = productDictionary
            };
            BasketOperations.BasketList.Add(basketDto);

            var amount = 1;

            //Act
            var result = _basketOperations.RemoveProduct(basketId, productId, amount);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void RemoveProduct_ShouldReturnFalse_WhenProductNotRemovedFromBasket()
        {
            //Arrange
            var productId = Guid.NewGuid();
            var productTitle = "Mobile Phone";
            var productAmount = 2;
            Dictionary<ProductModel, int> productDictionary = new Dictionary<ProductModel, int>();
            var productDto = new ProductModel
            {
                Id = productId,
                Title = productTitle
            };
            productDictionary.Add(productDto, productAmount);

            var basketId = Guid.NewGuid();
            var basketDto = new BasketModel
            {
                Id = basketId,
                Products = productDictionary
            };
            BasketOperations.BasketList.Add(basketDto);

            var amount = 1;

            //Act
            var result = _basketOperations.RemoveProduct(basketId, Guid.NewGuid(), amount);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void CalculateTotalDeliveryPrice_ShouldReturnCorrectValue_WhenCalculationCompleted()
        {
            //Arrange
            var categoryId = Guid.NewGuid();
            var categoryTitle = "Technology";
            var categoryDto = new CategoryModel
            {
                Id = categoryId,
                Title = categoryTitle
            };

            Dictionary<ProductModel, int> productDictionary = new Dictionary<ProductModel, int>();
            var productId = Guid.NewGuid();
            var productTitle = "Mobile Phone";
            var productAmount = 1;
            var productDto = new ProductModel
            {
                Id = productId,
                Title = productTitle,
                Category = categoryDto,
                Price = 4900
            };
            productDictionary.Add(productDto, productAmount);

            var basketId = Guid.NewGuid();
            var basketDto = new BasketModel
            {
                Id = basketId,
                Products = productDictionary
            };
            BasketOperations.BasketList.Add(basketDto);

            //Act
            var result = _basketOperations.CalculateTotalDeliveryPrice(basketDto);

            //Assert
            Assert.Equal(result, Convert.ToDecimal(4.2));
        }

        [Fact]
        public void CalculateTotalDeliveryPrice_ShouldReturnInCorrectValue_WhenCalculationCompleted()
        {
            //Arrange
            var categoryId = Guid.NewGuid();
            var categoryTitle = "Technology";
            var categoryDto = new CategoryModel
            {
                Id = categoryId,
                Title = categoryTitle
            };

            Dictionary<ProductModel, int> productDictionary = new Dictionary<ProductModel, int>();
            var productId = Guid.NewGuid();
            var productTitle = "Mobile Phone";
            var productAmount = 1;
            var productDto = new ProductModel
            {
                Id = productId,
                Title = productTitle,
                Category = categoryDto,
                Price = 4900
            };
            productDictionary.Add(productDto, productAmount);

            var basketId = Guid.NewGuid();
            var basketDto = new BasketModel
            {
                Id = basketId,
                Products = productDictionary
            };
            BasketOperations.BasketList.Add(basketDto);

            //Act
            var result = _basketOperations.CalculateTotalDeliveryPrice(basketDto);

            //Assert
            Assert.NotEqual(result, Decimal.One);
        }

        [Fact]
        public void ApplyCampaignToBasket_ShouldReturnTrue_WhenCampaignApplied()
        {
            //Arrange
            var categoryId = Guid.NewGuid();
            var categoryTitle = "Technology";
            var categoryDto = new CategoryModel
            {
                Id = categoryId,
                Title = categoryTitle
            };

            Dictionary<ProductModel, int> productDictionary = new Dictionary<ProductModel, int>();
            var productId = Guid.NewGuid();
            var productTitle = "Mobile Phone";
            var productAmount = 1;
            var productDto = new ProductModel
            {
                Id = productId,
                Title = productTitle,
                Category = categoryDto,
                Price = 4900
            };
            productDictionary.Add(productDto, productAmount);

            var campaignId = Guid.NewGuid();
            var campaignTitle = "Special discount for technology days!";
            var campaignType = CampaignType.Total;
            var campaignAmount = 250;
            var campaignDto = new CampaignModel
            {
                Id = campaignId,
                Title = campaignTitle,
                Type = campaignType,
                Amount = campaignAmount,
                CategoryIds = new List<Guid> {categoryId}
            };

            var basketId = Guid.NewGuid();
            var basketDto = new BasketModel
            {
                Id = basketId,
                Products = productDictionary
            };
            BasketOperations.BasketList.Add(basketDto);

            //Act
            var result = _basketOperations.ApplyCampaignToBasket(basketId, campaignDto);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void ApplyCampaignToBasket_ShouldReturnFalse_WhenCampaignNotApplied()
        {
            //Arrange
            var categoryId = Guid.NewGuid();
            var categoryTitle = "Technology";
            var categoryDto = new CategoryModel
            {
                Id = categoryId,
                Title = categoryTitle
            };

            Dictionary<ProductModel, int> productDictionary = new Dictionary<ProductModel, int>();
            var productId = Guid.NewGuid();
            var productTitle = "Mobile Phone";
            var productAmount = 1;
            var productDto = new ProductModel
            {
                Id = productId,
                Title = productTitle,
                Category = categoryDto,
                Price = 4900
            };
            productDictionary.Add(productDto, productAmount);

            var campaignId = Guid.NewGuid();
            var campaignTitle = "Special discount for technology days!";
            var campaignType = CampaignType.Total;
            var campaignAmount = 250;
            var campaignDto = new CampaignModel
            {
                Id = campaignId,
                Title = campaignTitle,
                Type = campaignType,
                Amount = campaignAmount
            };

            var basketId = Guid.NewGuid();
            var basketDto = new BasketModel
            {
                Id = basketId,
                Products = productDictionary
            };
            BasketOperations.BasketList.Add(basketDto);

            //Act
            var result = _basketOperations.ApplyCampaignToBasket(basketId, campaignDto);

            //Assert
            Assert.False(result);
        }

        [Fact]
        public void ApplyCouponToBasket_ShouldReturnTrue_WhenCouponApplied()
        {
            //Arrange
            var categoryId = Guid.NewGuid();
            var categoryTitle = "Technology";
            var categoryDto = new CategoryModel
            {
                Id = categoryId,
                Title = categoryTitle
            };

            Dictionary<ProductModel, int> productDictionary = new Dictionary<ProductModel, int>();
            var productId = Guid.NewGuid();
            var productTitle = "Mobile Phone";
            var productAmount = 1;
            var productDto = new ProductModel
            {
                Id = productId,
                Title = productTitle,
                Category = categoryDto,
                Price = 4900
            };
            productDictionary.Add(productDto, productAmount);

            var couponId = Guid.NewGuid();
            var couponType = CouponType.Rate;
            var couponAmount = 20;
            var minBasketPrice = 2500;
            var couponDto = new CouponModel
            {
                Id = couponId,
                Type = couponType,
                Amount = couponAmount,
                MinBasketPrice = minBasketPrice
            };


            var basketId = Guid.NewGuid();
            var basketDto = new BasketModel
            {
                Id = basketId,
                Products = productDictionary,
                SubTotal = productDto.Price * productAmount
            };
            BasketOperations.BasketList.Add(basketDto);

            //Act
            var result = _basketOperations.ApplyCouponToBasket(basketId, couponDto);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void ApplyCouponToBasket_ShouldReturnFalse_WhenCouponApplied()
        {
            //Arrange
            var categoryId = Guid.NewGuid();
            var categoryTitle = "Technology";
            var categoryDto = new CategoryModel
            {
                Id = categoryId,
                Title = categoryTitle
            };

            Dictionary<ProductModel, int> productDictionary = new Dictionary<ProductModel, int>();
            var productId = Guid.NewGuid();
            var productTitle = "Mobile Phone";
            var productAmount = 1;
            var productDto = new ProductModel
            {
                Id = productId,
                Title = productTitle,
                Category = categoryDto,
                Price = 4900
            };
            productDictionary.Add(productDto, productAmount);

            var couponId = Guid.NewGuid();
            var couponType = CouponType.Rate;
            var couponAmount = 20;
            var minBasketPrice = 5000;
            var couponDto = new CouponModel
            {
                Id = couponId,
                Type = couponType,
                Amount = couponAmount,
                MinBasketPrice = minBasketPrice
            };


            var basketId = Guid.NewGuid();
            var basketDto = new BasketModel
            {
                Id = basketId,
                Products = productDictionary,
                SubTotal = productDto.Price * productAmount
            };
            BasketOperations.BasketList.Add(basketDto);

            //Act
            var result = _basketOperations.ApplyCouponToBasket(basketId, couponDto);

            //Assert
            Assert.False(result);
        }
        
        [Fact]
        public void CalculateBasket_ShouldReturnCorrectBasketTotal_WhenCalculationCompleted()
        {
            var basketId = Guid.NewGuid();
            var basketDto = new BasketModel
            {
                Id = basketId,
                SubTotal = Convert.ToDecimal(340),
                CouponTotal = Convert.ToDecimal(55),
                DeliveryPrice = Convert.ToDecimal(11.1),
                DiscountTotal = Convert.ToDecimal(20.3)
            };
            BasketOperations.BasketList.Add(basketDto);

            //Act
            var result = _basketOperations.CalculateBasket(basketId);

            //Assert
            Assert.Equal(result.BasketTotal,Convert.ToDecimal(275.8));
        }
        
        [Fact]
        public void CalculateBasket_ShouldReturnInCorrectBasketTotal_WhenCalculationCompleted()
        {
            var basketId = Guid.NewGuid();
            var basketDto = new BasketModel
            {
                Id = basketId,
                SubTotal = Convert.ToDecimal(440),
                CouponTotal = Convert.ToDecimal(55),
                DeliveryPrice = Convert.ToDecimal(11.1),
                DiscountTotal = Convert.ToDecimal(20.3)
            };
            BasketOperations.BasketList.Add(basketDto);

            //Act
            var result = _basketOperations.CalculateBasket(basketId);

            //Assert
            Assert.NotEqual(result.BasketTotal,Convert.ToDecimal(275.8));
        }
    }
}