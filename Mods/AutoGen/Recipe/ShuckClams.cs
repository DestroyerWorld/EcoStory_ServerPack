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

    public class ShuckClamsRecipe : Recipe
    {
        public ShuckClamsRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<RawFishItem>(1f),  

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<ClamItem>(5)  
            };
            this.Initialize(Localizer.DoStr("Shuck Clams"), typeof(ShuckClamsRecipe));
            this.CraftMinutes = new ConstantValue(0.2f);
            CraftingComponent.AddRecipe(typeof(FisheryObject), this);
        }
    }
}