# What is ShoppingCard Project?
ShoppingCard is a basic e-commerce solution. The project was built on 3 main layers. 
 ## ShoppingCard
 ShoppingCard layer is contains the project configuration. Project startup, configs and dependency injections are managed through from this console.
 
 ## ShoppingCard.Core
The Shopping Card layer contains the project business logic. **Enums**, **Helpers**, **Interfaces**, **Models** and **Operations** folders are for business logic implementation. 

- **Enums** : Define campaign and cupon types. (Total and Rate)
    * CampaignType
    * CouponType
- **Helpers** : Help to Basket Manager
    * BasketHelper
- **Interfaces** : Created for all operations declaration.
    * IBasketOperation
    * ICampaignOperation
    * ICategoryOperation
    * ICouponOperation
    * IProductOperation
- **Models** : It represents the shape of the operations data and business logic as methods. 
    * BasketModel
    * CampaignModel
    * CategoryModel
    * CouponModel
    * ProductModel
- **Operations** : It represents the manage of the **operations** data and **business logic** as methods.
    * BasketOperation
    * CampaignOperation
    * CategoryOperation
    * CouponOperation
    * ProductOperation
- **BasketManager** : It is the class in which the **flow** is managed from end to end.
- **Settings** : It is contains settings for the project.
 
 ## ShoppingCard.Tests
 This project tests the operation classes.
 - **Tests** : It contains unit tests for all operation methods. Uses Xunit technology.
    * BasketOperationTests
    * CampaignOperationTests
    * CategoryOperationTests
    * CouponOperationTests
    * ProductOperationTests
