using System;
using System.Collections.Generic;
using System.Linq;
using ShoppingCard.Core.Enums;
using ShoppingCard.Core.Interfaces;
using ShoppingCard.Core.Models;

namespace ShoppingCard.Core.Operations
{
    public class CampaignOperations : ICampaignOperations
    {
        private static readonly List<CampaignModel> CampaignList = new List<CampaignModel>();

        public CampaignModel GetCampaign(Guid campaignId)
        {
            return CampaignList?.FirstOrDefault(a => a.Id == campaignId);
        }
        public CampaignModel AddCampaign(CampaignType type,decimal amount,string campaignTitle,int minimumCount)
        {
            CampaignModel campaign = new CampaignModel()
            {
                Id = Guid.NewGuid(),
                Type = type,
                Amount = amount,
                Title = campaignTitle,
                MinimumProductCount = minimumCount
            };
            CampaignList.Add(campaign);
            return campaign;
        }
        public bool RemoveCampaign(Guid campaignId)
        {
            var campaign = GetCampaign(campaignId);
            if (campaign != null)
            {
                CampaignList.Remove(campaign);
                return true;
            }
            return false;
        }
        public bool BindCategories(Guid campaignId, Guid categoryIds)
        {
            var campaign = GetCampaign(campaignId);
            if (campaign != null)
            {
                campaign.CategoryIds = new List<Guid>{categoryIds};
                return true;
            }

            return false;
        }
    }
}