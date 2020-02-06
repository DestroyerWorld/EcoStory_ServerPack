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

    public partial class PropertyToolRecipe : Recipe
    {
        public PropertyToolRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<PropertyToolItem>(),          
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<LogItem>(10)    
            };

            this.CraftMinutes = new ConstantValue(0.5f);
            this.Initialize(Localizer.DoStr("Property Tool"), typeof(PropertyToolRecipe));

            CraftingComponent.AddRecipe(typeof(WorkbenchObject), this);
        }
    }

    [Serialized]
    [Weight(1000)]      
    [Currency]              
    public partial class PropertyToolItem :
    ToolItem                        
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Land Claim Stake"); } } 
        public override LocString DisplayNamePlural { get { return Localizer.DoStr("Land Claim Stakes"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("Used to claim, unclaim, and examine property."); } }
    }
}