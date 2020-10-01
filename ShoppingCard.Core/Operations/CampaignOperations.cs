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
        public static List<CampaignModel> CampaignList = new List<CampaignModel>();

        public CampaignModel GetCampaign(Guid campaignId)
        {
            return CampaignList?.FirstOrDefault(a => a.Id == campaignId);
        }

        public CampaignModel AddCampaign(CampaignType type, decimal amount, string campaignTitle, int minimumCount)
        {
            if (amount >= 0 && minimumCount >= 0)
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

            Console.WriteLine("Kampanya eklenemedi. Mikar, minimum ürün tutarı negatif olamaz.");
            return null;
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

        public bool BindCategories(Guid campaignId, Guid categoryId)
        {
            var campaign = GetCampaign(campaignId);
            if (campaign != null && categoryId != Guid.Empty) 
            {
                campaign.CategoryIds = new List<Guid> {categoryId};
                return true;
            }

            return false;
        }
    }
}