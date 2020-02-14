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
    public class CampfireSalmonRecipe : Recipe
    {
        public CampfireSalmonRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<CharredFishItem>(2f),  

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<SalmonItem>(1)  
            };
            this.Initialize(Localizer.DoStr("Campfire Salmon"), typeof(CampfireSalmonRecipe));
            this.CraftMinutes = new ConstantValue(5);
            CraftingComponent.AddRecipe(typeof(CampfireObject), this);
        }
    }
}