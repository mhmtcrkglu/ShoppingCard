using System;
using Newtonsoft.Json;
using ShoppingCard.Core.Enums;
using ShoppingCard.Core.Interfaces;


namespace ShoppingCard.Core
{
    public class BasketManager
    {
        private readonly IBasketOperations _basketOperations;
        private readonly ICampaignOperations _campaignOperations;
        private readonly ICategoryOperations _categoryOperations;
        private readonly ICouponOperations _couponOperations;
        private readonly IProductOperations _productOperations;

        public BasketManager(IBasketOperations basketOperations, ICampaignOperations campaignOperations,
            ICategoryOperations categoryOperations, ICouponOperations couponOperations,
            IProductOperations productOperations)
        {
            _basketOperations = basketOperations;
            _campaignOperations = campaignOperations;
            _categoryOperations = categoryOperations;
            _couponOperations = couponOperations;
            _productOperations = productOperations;
        }

        public void Run()
        {
            #region Create Categories

            var woman = _categoryOperations.AddCategory("Woman");
            var man = _categoryOperations.AddCategory("Man");
            var beauty = _categoryOperations.AddCategory("Beauty");
            var tShirt = _categoryOperations.AddCategory("T-Shirts");
            var summer = _categoryOperations.AddCategory("Summer");

            #endregion

            #region BindCategoryToCategory

            _categoryOperations.BindCategory(tShirt.Id, man.Id);
            _categoryOperations.BindCategory(beauty.Id, woman.Id);

            #endregion
            
            #region Create Products
            
            var maybelLine = _productOperations.AddProduct("Maybelline-Lash Sensational Yelpaze Etkili Intense Black Maskara", Convert.ToDecimal(69.90));
            var trendyolMan = _productOperations.AddProduct("TRENDYOL MAN-Gri Erkek Yakası Kontrast Polo Yaka T-Shirt", Convert.ToDecimal(32.99));
            var sunCream = _productOperations.AddProduct("Sebamed-Güneş Koruyucu Krem ", Convert.ToDecimal(89.90));
            
            #endregion
            
            #region BindProductToCategory
            
            _productOperations.BindCategory(maybelLine.Id, beauty);
            _productOperations.BindCategory(trendyolMan.Id, tShirt);
            _productOperations.BindCategory(sunCream.Id, summer);
            
            #endregion
            
            #region Create Basket
            
            var basketId = _basketOperations.AddBasket();
            
            #endregion
            
            #region BindProductToBasket
            
            _basketOperations.BindProduct(basketId, maybelLine, 2);
            _basketOperations.BindProduct(basketId, trendyolMan, 3);
            _basketOperations.BindProduct(basketId, sunCream, 1);
            
            #endregion
            
            #region CreateCampaign
            
            var manBasketCampaign = _campaignOperations.AddCampaign(CampaignType.Rate, Convert.ToDecimal(25), "Sepette %25 İndirim", 1);
            var beautyCampaign = _campaignOperations.AddCampaign(CampaignType.Total, Convert.ToDecimal(20), "Sepette 20TL İndirim", 3);
            #endregion
            
            #region BindCampaignToCategory
            
            _campaignOperations.BindCategories(manBasketCampaign.Id,tShirt.Id);
            _campaignOperations.BindCategories(beautyCampaign.Id, beauty.Id);
            
            #endregion

            #region CreateCoupon
            
            var bigChance = _couponOperations.AddCoupon(CouponType.Rate, 5, 0);
            var summerCoupon = _couponOperations.AddCoupon(CouponType.Total, 20, 150);
            
            #endregion
            
            #region ApplyCampaignToBasket
            
            _basketOperations.ApplyCampaignToBasket(basketId, manBasketCampaign);
            _basketOperations.ApplyCampaignToBasket(basketId, beautyCampaign);
            
            #endregion
            
            #region ApplyCouponToBasket
            
            _basketOperations.ApplyCouponToBasket(basketId, bigChance);
            _basketOperations.ApplyCouponToBasket(basketId, summerCoupon);
            
            #endregion
            
            var basket = _basketOperations.CalculateBasket(basketId);
            string output = JsonConvert.SerializeObject(basket);
            Console.WriteLine("Basket is ready, total price is {0}",basket.BasketTotal);
        }
    }
}