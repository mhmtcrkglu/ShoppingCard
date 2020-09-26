using System;
using System.Collections.Generic;
using System.Linq;
using ShoppingCard.Core.Interfaces;
using ShoppingCard.Core.Models;

namespace ShoppingCard.Core.Operations
{
    public class CampaignOperations : ICampaignOperations
    {
        public static List<CampaignModel> _campaignList;
        
        public CampaignOperations()
        {
        }

        public CampaignModel GetCampaign(Guid campaignId)
        {
            return _campaignList?.FirstOrDefault(a => a.Id == campaignId);
        }
        
        public Guid AddCampaign(string campaignTitle, int minimumAmount)
        {
            CampaignModel campaign = new CampaignModel()
            {
                Id = Guid.NewGuid(),
                Title = campaignTitle,
                MinimumProductCount = minimumAmount
            };
            _campaignList.Add(campaign);
            return campaign.Id;
        }

        public bool RemoveCampaign(Guid campaignId)
        {
            var campaign = GetCampaign(campaignId);
            if (campaign != null)
            {
                _campaignList.Remove(campaign);
                return true;
            }
            return false;
        }

        public bool BindCategories(Guid campaignId, List<Guid> categoryIds)
        {
            var campaign = GetCampaign(campaignId);
            if (campaign != null)
            {
                campaign.CategoryIds = categoryIds;
                return true;
            }

            return false;
        }
    }
}