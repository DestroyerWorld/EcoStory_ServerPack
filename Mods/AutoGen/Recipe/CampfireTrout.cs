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

    public class CampfireTroutRecipe : Recipe
    {
        public CampfireTroutRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<CharredFishItem>(1f),  

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<TroutItem>(1)  
            };
            this.Initialize(Localizer.DoStr("Campfire Trout"), typeof(CampfireTroutRecipe));
            this.CraftMinutes = new ConstantValue(3);
            CraftingComponent.AddRecipe(typeof(CampfireObject), this);
        }
    }
}