namespace Eco.Mods.TechTree
{
    // [DoNotLocalize]
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Eco.Gameplay.Blocks;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.World;
    using Eco.World.Blocks;
    using Eco.Gameplay.Pipes;

    public partial class DirtRampRecipe : Recipe
    {
        public DirtRampRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<DirtRampItem>(),          
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<DirtItem>(6)    
            };
            this.CraftMinutes = new ConstantValue(0.5f);
            this.Initialize(Localizer.DoStr("Dirt Ramp"), typeof(DirtRampRecipe));

            CraftingComponent.AddRecipe(typeof(WainwrightTableObject), this);
            CraftingComponent.AddRecipe(typeof(WorkbenchObject), this);
        }
    }

    [Serialized]
    [Constructed]
    [Road(1f)]                                          
    public partial class DirtRampBlock :
        Block            
    {
    }

}