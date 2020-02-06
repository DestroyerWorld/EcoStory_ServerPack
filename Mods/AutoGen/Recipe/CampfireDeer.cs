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

    public class CampfireDeerRecipe : Recipe
    {
        public CampfireDeerRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<CharredMeatItem>(4f),  
               new CraftingElement<TallowItem>(2f),  

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<DeerCarcassItem>(1)  
            };
            this.Initialize(Localizer.DoStr("Campfire Deer"), typeof(CampfireDeerRecipe));
            this.CraftMinutes = new ConstantValue(4);
            CraftingComponent.AddRecipe(typeof(CampfireObject), this);
        }
    }
}