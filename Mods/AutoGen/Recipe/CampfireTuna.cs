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
    public class CampfireTunaRecipe : Recipe
    {
        public CampfireTunaRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<CharredFishItem>(3f),  

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<TunaItem>(1)  
            };
            this.Initialize(Localizer.DoStr("Campfire Tuna"), typeof(CampfireTunaRecipe));
            this.CraftMinutes = new ConstantValue(10);
            CraftingComponent.AddRecipe(typeof(CampfireObject), this);
        }
    }
}