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
    public class CampfireOtterRecipe : Recipe
    {
        public CampfireOtterRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<CharredMeatItem>(1f),  
               new CraftingElement<TallowItem>(1f),  

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<OtterCarcassItem>(1)  
            };
            this.Initialize(Localizer.DoStr("Campfire Otter"), typeof(CampfireOtterRecipe));
            this.CraftMinutes = new ConstantValue(2);
            CraftingComponent.AddRecipe(typeof(CampfireObject), this);
        }
    }
}