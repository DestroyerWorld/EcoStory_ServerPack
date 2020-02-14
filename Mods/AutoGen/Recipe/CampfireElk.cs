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
    public class CampfireElkRecipe : Recipe
    {
        public CampfireElkRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<CharredMeatItem>(4f),  
               new CraftingElement<TallowItem>(2f),  

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<ElkCarcassItem>(1)  
            };
            this.Initialize(Localizer.DoStr("Campfire Elk"), typeof(CampfireElkRecipe));
            this.CraftMinutes = new ConstantValue(10);
            CraftingComponent.AddRecipe(typeof(CampfireObject), this);
        }
    }
}