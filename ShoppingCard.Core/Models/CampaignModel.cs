using System;
using System.Collections.Generic;

namespace ShoppingCard.Core.Models
{
    public class CampaignModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public List<Guid> CategoryIds { get; set; }
        public int MinimumProductCount { get; set; }
    }
}