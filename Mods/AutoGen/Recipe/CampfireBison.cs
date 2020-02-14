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
    public class CampfireBisonRecipe : Recipe
    {
        public CampfireBisonRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<CharredMeatItem>(8f),  
               new CraftingElement<TallowItem>(4f),  

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<BisonCarcassItem>(1)  
            };
            this.Initialize(Localizer.DoStr("Campfire Bison"), typeof(CampfireBisonRecipe));
            this.CraftMinutes = new ConstantValue(20);
            CraftingComponent.AddRecipe(typeof(CampfireObject), this);
        }
    }
}