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


    [Serialized]
    [Solid, Wall, Minable(4)]
    public partial class GneissBlock :
        Block            
        , IRepresentsItem   
    {
        public Type RepresentedItemType { get { return typeof(GneissItem); } }  
    }

    [Serialized]
    [MaxStackSize(20)]                           
    [Weight(7000)]      
    [ResourcePile]                                          
    [Currency]              
    public partial class GneissItem :
    BlockItem<GneissBlock>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Gneiss"); } } 
        public override LocString DisplayNamePlural { get { return Localizer.DoStr("Gneiss"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A hard rock with some uses in construction. Gneiss is a metamorphic rock formed from previous rock recrystallizing at high pressures and temperatures deep in the earth."); } }

        public override bool CanStickToWalls { get { return false; } }  

        private static Type[] blockTypes = new Type[] {
            typeof(GneissStacked1Block),
            typeof(GneissStacked2Block),
            typeof(GneissStacked3Block),
            typeof(GneissStacked4Block)
        };
        public override Type[] BlockTypes { get { return blockTypes; } }
    }

    [Serialized, Solid] public class GneissStacked1Block : PickupableBlock { }
    [Serialized, Solid] public class GneissStacked2Block : PickupableBlock { }
    [Serialized, Solid] public class GneissStacked3Block : PickupableBlock { }
    [Serialized, Solid,Wall] public class GneissStacked4Block : PickupableBlock { } //Only a wall if it's all 4 Gneiss
}