using System;
using System.Collections.Generic;
using ShoppingCard.Core.Enums;

namespace ShoppingCard.Core.Models
{
    public class CampaignModel
    {
        public Guid Id { get; set; }
        public CampaignType Type { get; set; }
        public decimal Amount { get; set; }
        public string Title { get; set; }
        public List<Guid> CategoryIds { get; set; }
        public int MinimumProductCount { get; set; }
    }
}