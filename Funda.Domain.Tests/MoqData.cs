using Funda.Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Funda.Domain.Tests
{
    public static class MoqData
    {
        public static  PropertyListingResponse MoqListings = new PropertyListingResponse
        {
            Properties = new List<Property>
                {
                    new Property
                    {
                        EstateAgentId = 1,
                        EstateAgentName = "agent1",
                        PromotionLabel = new PromoLabel
                        {
                            HasPromotionLabel = true,
                            Tagline = "tagline"
                        }
                    },
                    new Property
                    {
                        EstateAgentId = 1,
                        EstateAgentName = "agent1",
                        PromotionLabel = new PromoLabel
                        {
                            HasPromotionLabel = true,
                            Tagline = "tagline"
                        }
                    },
                    new Property
                    {
                        EstateAgentId = 1,
                        EstateAgentName = "agent1",
                        PromotionLabel = new PromoLabel
                        {
                            HasPromotionLabel = true,
                            Tagline = "tagline"
                        }
                    },
                    new Property
                    {
                        EstateAgentId = 2,
                        EstateAgentName = "agent2",
                        PromotionLabel = new PromoLabel
                        {
                            HasPromotionLabel = true,
                            Tagline = "tagline"
                        }
                    },
                    new Property
                    {
                        EstateAgentId = 2,
                        EstateAgentName = "agent2",
                        PromotionLabel = new PromoLabel
                        {
                            HasPromotionLabel = false,
                            Tagline = null
                        }
                    },
                    new Property
                    {
                        EstateAgentId = 2,
                        EstateAgentName = "agent2",
                        PromotionLabel = new PromoLabel
                        {
                            HasPromotionLabel = true,
                            Tagline = "listing with garden"
                        }
                    },
                    new Property
                    {
                        EstateAgentId = 3,
                        EstateAgentName = "agent3",
                        PromotionLabel = new PromoLabel
                        {
                            HasPromotionLabel = true,
                            Tagline = "listing with garden"
                        }
                    },
                    new Property
                    {
                        EstateAgentId = 3,
                        EstateAgentName = "agent3",
                        PromotionLabel = new PromoLabel
                        {
                            HasPromotionLabel = true,
                            Tagline = "listing with garden"
                        }
                    },
                    new Property
                    {
                        EstateAgentId = 4,
                        EstateAgentName = "agent4",
                        PromotionLabel = new PromoLabel
                        {
                            HasPromotionLabel = true,
                            Tagline = "listing with garden"
                        }
                    }
                }
        };
        
        
    }
}
