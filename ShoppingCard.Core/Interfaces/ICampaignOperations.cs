using System;
using System.Collections.Generic;
using ShoppingCard.Core.Enums;
using ShoppingCard.Core.Models;

namespace ShoppingCard.Core.Interfaces
{
    public interface ICampaignOperations
    {
        public CampaignModel GetCampaign(Guid campaignId);
        public CampaignModel AddCampaign(CampaignType type,decimal amount,string campaignTitle,int minimumCount);
        public bool RemoveCampaign(Guid campaignId);
        public bool BindCategories(Guid campaignId, Guid categoryIds);
    }
}