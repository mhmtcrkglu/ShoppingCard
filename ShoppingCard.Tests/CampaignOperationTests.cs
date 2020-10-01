using System;
using ShoppingCard.Core.Enums;
using ShoppingCard.Core.Models;
using ShoppingCard.Core.Operations;
using Xunit;

namespace ShoppingCard.Tests
{
    public class CampaignOperationTests
    {
        private readonly CampaignOperations _campaignOperations = new CampaignOperations();

        [Fact]
        public void GetCampaignById_ShouldReturnCampaign_WhenCampaignExists()
        {
            //Arrange
            var campaignId = Guid.NewGuid();
            var campaignTitle = "Summer Campaign";
            var amount = 5;
            var campaignDto = new CampaignModel
            {
                Id = campaignId,
                Title = campaignTitle,
                Amount = amount
            };
            CampaignOperations.CampaignList.Add(campaignDto);

            //Act
            var campaign = _campaignOperations.GetCampaign(campaignId);

            //Assert
            Assert.Equal(campaignId, campaign.Id);
        }

        [Fact]
        public void GetCampaignById_ShouldReturnCampaign_WhenCampaignDoesNotExists()
        {
            //Arrange
            CampaignOperations.CampaignList.Add(new CampaignModel());

            //Act
            var campaign = _campaignOperations.GetCampaign(Guid.NewGuid());

            //Assert
            Assert.Null(campaign);
        }

        [Fact]
        public void AddCampaign_ShouldReturnCampaignModel_WhenCampaignAddedSuccess()
        {
            //Arrange
            var campaignType = CampaignType.Rate;
            var campaignTitle = "Summer Campaign";
            var amount = 10;
            var minimumCount = 2;

            //Act
            var result = _campaignOperations.AddCampaign(campaignType, amount, campaignTitle, minimumCount);

            //Assert
            Assert.NotNull(result);
        }
        
        [Fact]
        public void AddCampaign_ShouldReturnNull_WhenCampaignAddedFail()
        {
            //Arrange
            var campaignType = CampaignType.Rate;
            var campaignTitle = "Summer Campaign";
            var amount = -10;
            var minimumCount = 2;

            //Act
            var result = _campaignOperations.AddCampaign(campaignType, amount, campaignTitle, minimumCount);

            //Assert
            Assert.Null(result);
        }
        
        [Fact]
        public void RemoveCampaign_ShouldReturnTrue_WhenCampaignExists()
        {
            //Arrange
            var campaignId = Guid.NewGuid();
            var campaignTitle = "Summer Campaign";
            var amount = 5;
            var campaignDto = new CampaignModel
            {
                Id = campaignId,
                Title = campaignTitle,
                Amount = amount
            };
            CampaignOperations.CampaignList.Add(campaignDto);

            //Act
            var result = _campaignOperations.RemoveCampaign(campaignDto.Id);

            //Assert
            Assert.True(result);
        }
        
        [Fact]
        public void RemoveCampaign_ShouldReturnFalse_WhenCampaignNotExists()
        {
            //Arrange
            var campaignId = Guid.NewGuid();
            var campaignTitle = "Summer Campaign";
            var amount = 5;
            var campaignDto = new CampaignModel
            {
                Id = campaignId,
                Title = campaignTitle,
                Amount = amount
            };
            CampaignOperations.CampaignList.Add(campaignDto);

            //Act
            var result = _campaignOperations.RemoveCampaign(Guid.NewGuid());

            //Assert
            Assert.False(result);
        }
        
        [Fact]
        public void BindCampaign_ShouldReturnTrue_WhenCategoryExists()
        {
            //Arrange
            var campaignId = Guid.NewGuid();
            var campaignTitle = "Summer Campaign";
            var amount = 5;
            var campaignDto = new CampaignModel
            {
                Id = campaignId,
                Title = campaignTitle,
                Amount = amount
            };
            CampaignOperations.CampaignList.Add(campaignDto);
            
            var categoryId = Guid.NewGuid();
            var categoryTitle = "Technology";
            var parentCampaignDto = new CategoryModel
            {
                Id = categoryId,
                Title = categoryTitle
            };

            //Act
            var result = _campaignOperations.BindCampaignToCategory(campaignId,categoryId);

            //Assert
            Assert.True(result);
        }
        
        [Fact]
        public void BindCampaign_ShouldReturnFalse_WhenCategoryNotExists()
        {
            //Arrange
            var campaignId = Guid.NewGuid();
            var campaignTitle = "Summer Campaign";
            var amount = 5;
            var campaignDto = new CampaignModel
            {
                Id = campaignId,
                Title = campaignTitle,
                Amount = amount
            };
            CampaignOperations.CampaignList.Add(campaignDto);

            //Act
            var result = _campaignOperations.BindCampaignToCategory(campaignId,Guid.Empty);

            //Assert
            Assert.False(result);
        }
    }
}