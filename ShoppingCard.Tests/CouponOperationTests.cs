using System;
using ShoppingCard.Core.Enums;
using ShoppingCard.Core.Models;
using ShoppingCard.Core.Operations;
using Xunit;

namespace ShoppingCard.Tests
{
    public class CouponOperationTests
    {
        private readonly CouponOperations _couponOperations = new CouponOperations();

        [Fact]
        public void GetCouponById_ShouldReturnCoupon_WhenCouponExists()
        {
            //Arrange
            var couponId = Guid.NewGuid();
            var amount = 5;
            var minBasketPrice = 100;
            var couponType = CouponType.Rate;
            var couponDto = new CouponModel
            {
                Id = couponId,
                Amount = amount,
                Type = couponType,
                MinBasketPrice = minBasketPrice
            };
            
            CouponOperations.CouponList.Add(couponDto);
            
            //Act
            var coupon = _couponOperations.GetCoupon(couponId);
            
            //Assert
            Assert.Equal(couponId,coupon.Id);
        }
        
        [Fact]
        public void GetCouponById_ShouldReturnCoupon_WhenCouponDoesNotExists()
        {
            //Arrange
            CouponOperations.CouponList.Add(new CouponModel());
            
            //Act
            var coupon = _couponOperations.GetCoupon(Guid.NewGuid());
            
            //Assert
            Assert.Null(coupon);
        }
        
        [Fact]
        public void AddCoupon_ShouldReturnCouponModel_WhenCouponAddedSuccess()
        {
            //Arrange
            var amount = 5;
            var minBasketPrice = 100;
            var couponType = CouponType.Rate;

            //Act
            var result = _couponOperations.AddCoupon(couponType,amount,minBasketPrice);

            //Assert
            Assert.NotNull(result);
        }
        
        [Fact]
        public void AddCoupon_ShouldReturnNull_WhenCouponAddedFail()
        {
            //Arrange
            var couponAmount = -5;
            var couponMinimumCount = 2;
            var couponType = CouponType.Rate;


            //Act
            var result = _couponOperations.AddCoupon(couponType,couponAmount,couponMinimumCount);

            //Assert
            Assert.Null(result);
        }
        
        [Fact]
        public void RemoveCoupon_ShouldReturnTrue_WhenCouponExists()
        {
            //Arrange
            var couponId = Guid.NewGuid();
            var couponAmount = 50;
            var couponType = CouponType.Total;
            var minBasketPrice = 250;
            
            var couponDto = new CouponModel
            {
                Id = couponId,
                Amount = couponAmount,
                Type = couponType,
                MinBasketPrice = minBasketPrice
                
            };
            CouponOperations.CouponList.Add(couponDto);

            //Act
            var result = _couponOperations.RemoveCoupon(couponDto.Id);

            //Assert
            Assert.True(result);
        }
        
        [Fact]
        public void RemoveCoupon_ShouldReturnFalse_WhenCouponNotExists()
        {
            //Arrange
            var couponId = Guid.NewGuid();
            var couponAmount = 50;
            var couponType = CouponType.Total;
            var minBasketPrice = 250;
            
            var couponDto = new CouponModel
            {
                Id = couponId,
                Amount = couponAmount,
                Type = couponType,
                MinBasketPrice = minBasketPrice
                
            };
            CouponOperations.CouponList.Add(couponDto);

            //Act
            var result = _couponOperations.RemoveCoupon(new Guid());

            //Assert
            Assert.True(result);
        }
    }
}