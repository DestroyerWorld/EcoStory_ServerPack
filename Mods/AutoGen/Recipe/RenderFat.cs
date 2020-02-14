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

    [RequiresSkill(typeof(AdvancedCampfireCookingSkill), 1)] 
    public class RenderFatRecipe : Recipe
    {
        public RenderFatRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<TallowItem>(2f),  

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<RawMeatItem>(typeof(AdvancedCampfireCookingSkill), 6, AdvancedCampfireCookingSkill.MultiplicativeStrategy, typeof(AdvancedCampfireCookingLavishResourcesTalent)), 
            };
            this.Initialize(Localizer.DoStr("Render Fat"), typeof(RenderFatRecipe));
            this.ExperienceOnCraft = 0.5f;  
            this.CraftMinutes = CreateCraftTimeValue(typeof(RenderFatRecipe), this.UILink(), 2, typeof(AdvancedCampfireCookingSkill), typeof(AdvancedCampfireCookingFocusedSpeedTalent), typeof(AdvancedCampfireCookingParallelSpeedTalent));    
            CraftingComponent.AddRecipe(typeof(CampfireObject), this);
        }
    }
}