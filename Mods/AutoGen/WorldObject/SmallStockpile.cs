namespace Eco.Mods.TechTree
{
    // [DoNotLocalize]
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Eco.Gameplay.Blocks;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Economy;
    using Eco.Gameplay.Housing;
    using Eco.Gameplay.Interactions;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Minimap;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Property;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Gameplay.Pipes.LiquidComponents;
    using Eco.Gameplay.Pipes.Gases;
    using Eco.Gameplay.Systems.Tooltip;
    using Eco.Shared;
    using Eco.Shared.Math;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.Shared.View;
    using Eco.Shared.Items;
    using Eco.Gameplay.Pipes;
    using Eco.World.Blocks;
    

    [Serialized]
    public partial class SmallStockpileItem :
        WorldObjectItem<SmallStockpileObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Small Stockpile"); } } 
        public override LocString DisplayDescription  { get { return Localizer.DoStr("Designates a 3x3x3 area as storage for large items."); } }

        static SmallStockpileItem()
        {
            
        }

        
    }

    public partial class SmallStockpileRecipe : Recipe
    {
        public SmallStockpileRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<SmallStockpileItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<LogItem>(5)                                                                     
            };
            this.CraftMinutes = new ConstantValue(1);
            this.Initialize(Localizer.DoStr("Small Stockpile"), typeof(SmallStockpileRecipe));
            CraftingComponent.AddRecipe(typeof(WorkbenchObject), this);
        }
    }
}