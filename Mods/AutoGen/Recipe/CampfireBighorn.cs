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
    public class CampfireBighornRecipe : Recipe
    {
        public CampfireBighornRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<CharredMeatItem>(4f),  
               new CraftingElement<TallowItem>(2f),  

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<BighornCarcassItem>(1)  
            };
            this.Initialize(Localizer.DoStr("Campfire Bighorn"), typeof(CampfireBighornRecipe));
            this.CraftMinutes = new ConstantValue(4);
            CraftingComponent.AddRecipe(typeof(CampfireObject), this);
        }
    }
}