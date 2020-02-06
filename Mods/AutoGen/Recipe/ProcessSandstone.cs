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

    public class ProcessSandstoneRecipe : Recipe
    {
        public ProcessSandstoneRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<StoneItem>(1f),  

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<SandstoneItem>(1)  
            };
            this.Initialize(Localizer.DoStr("Process Sandstone"), typeof(ProcessSandstoneRecipe));
            this.CraftMinutes = new ConstantValue(0.04f);
            CraftingComponent.AddRecipe(typeof(WorkbenchObject), this);
        }
    }
}