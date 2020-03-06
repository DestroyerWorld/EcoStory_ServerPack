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
    using Eco.Gameplay.Pipes.LiquidComponents; 

    [RequiresSkill(typeof(OilDrillingSkill), 1)]      
    public partial class PetroleumRecipe : Recipe
    {
        public PetroleumRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<PetroleumItem>(),          
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<BarrelItem>(1), 
            };
            this.ExperienceOnCraft = 0.5f;  
            this.CraftMinutes = new MultiDynamicValue(MultiDynamicOps.Multiply
            , CreateCraftTimeValue(typeof(PetroleumRecipe), Item.Get<PetroleumItem>().UILink(), 30, typeof(OilDrillingSkill), typeof(OilDrillingFocusedSpeedTalent), typeof(OilDrillingParallelSpeedTalent))    
            , new LayerModifiedValue(Eco.Simulation.WorldLayers.LayerNames.Oil,3)    
            );
            this.Initialize(Localizer.DoStr("Petroleum"), typeof(PetroleumRecipe));

            CraftingComponent.AddRecipe(typeof(PumpJackObject), this);
        }
    }

    [Serialized]
    [Solid]
    [RequiresSkill(typeof(OilDrillingSkill), 0)]   
    public partial class PetroleumBlock :
        PickupableBlock      
        , IRepresentsItem   
    {
        public Type RepresentedItemType { get { return typeof(PetroleumItem); } }  
    }

    [Serialized]
    [MaxStackSize(10)]                                       
    [Weight(30000)]      
    [Fuel(80000)][Tag("Fuel")]          
    [Currency]              
    public partial class PetroleumItem :
    BlockItem<PetroleumBlock>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Petroleum"); } } 
        public override LocString DisplayNamePlural { get { return Localizer.DoStr("Petroleum"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A fossil fuel essential for manufacturing gasoline, plastics, and asphalt. It's extraction, transport, and burning all have environmental impacts."); } }

    }

}