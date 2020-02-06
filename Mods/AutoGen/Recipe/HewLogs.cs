namespace Eco.Mods.TechTree
{
    // [DoNotLocalize]
    using System;
    using System.Collections.Generic;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Utils;
    using Eco.World;
    using Eco.World.Blocks;
    using Gameplay.Systems.TextLinks;
    using Eco.Shared.Localization;

    [RequiresSkill(typeof(HewingSkill), 0)] 
    public class HewLogsRecipe : Recipe
    {
        public HewLogsRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<HewnLogItem>(1f),  

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<LogItem>(typeof(HewingSkill), 2, HewingSkill.MultiplicativeStrategy, typeof(HewingLavishResourcesTalent)), 
            };
            this.Initialize(Localizer.DoStr("Hew Logs"), typeof(HewLogsRecipe));
            this.ExperienceOnCraft = 0.5f;  
            this.CraftMinutes = CreateCraftTimeValue(typeof(HewLogsRecipe), this.UILink(), 0.3f, typeof(HewingSkill), typeof(HewingFocusedSpeedTalent), typeof(HewingParallelSpeedTalent));    
            CraftingComponent.AddRecipe(typeof(CarpentryTableObject), this);
        }
    }
}