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

    public class ProcessShaleRecipe : Recipe
    {
        public ProcessShaleRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<StoneItem>(1f),  

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<ShaleItem>(1)  
            };
            this.Initialize(Localizer.DoStr("Process Shale"), typeof(ProcessShaleRecipe));
            this.CraftMinutes = new ConstantValue(0.04f);
            CraftingComponent.AddRecipe(typeof(WorkbenchObject), this);
        }
    }
}