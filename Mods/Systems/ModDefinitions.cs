// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.

namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Housing;
    using Eco.Core.Plugins.Interfaces;
    using System.Collections.Generic;
    using System.Linq;
    using Eco.Gameplay.Systems;
    using Gameplay.Items;

    public class ModDefinitions : IModInit
    {
        public static void Initialize()
        {
            PlayerHousingManager.SetHousingTiers(new[]
            {
                new HousingTier { TierVal = 0, SoftCap = 2f,  HardCap = 4f,  DiminishingReturnPercent = .5f },
                new HousingTier { TierVal = 1, SoftCap = 5f,  HardCap = 10f, DiminishingReturnPercent = .5f },
                new HousingTier { TierVal = 2, SoftCap = 10f, HardCap = 20f, DiminishingReturnPercent = .5f },
                new HousingTier { TierVal = 3, SoftCap = 15f, HardCap = 30f, DiminishingReturnPercent = .5f },
                new HousingTier { TierVal = 4, SoftCap = 20f, HardCap = 40f, DiminishingReturnPercent = .5f }
            });
        }

        public static void PostInitialize()
        {
            var categoryToTags = TagAttribute.CategoryToTags ?? new Dictionary<string, string[]>();
            var tiers          = new HashSet<float> { 0 };
            foreach (var item in Item.AllItems)
            {
                if (item.Hidden) continue;
                var itemTier = ItemAttribute.Get<ItemTier>(item.Type);
                if (itemTier != null)
                    tiers.Add(itemTier.Tier);
            }

            categoryToTags["Tiers"] = tiers.OrderBy(x => x).Select(x => string.Format("Tier {0}", x)).ToArray();
            TagAttribute.CategoryToTags = categoryToTags;
        }
    }
}