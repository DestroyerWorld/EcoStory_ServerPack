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

    public class CampfireWolfRecipe : Recipe
    {
        public CampfireWolfRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<CharredMeatItem>(3f),  
               new CraftingElement<TallowItem>(2f),  

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<WolfCarcassItem>(1)  
            };
            this.Initialize(Localizer.DoStr("Campfire Wolf"), typeof(CampfireWolfRecipe));
            this.CraftMinutes = new ConstantValue(2);
            CraftingComponent.AddRecipe(typeof(CampfireObject), this);
        }
    }
}